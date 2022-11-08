using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tivote.Data;
using Tivote.Models;
using Tivote.Models.Admin;
using Tivote.Models.ViewModels;
using TivPortal.ViewComponents;

namespace Tivote.Controllers
{
    public class ContactsController : MainController
    {
        public ContactsController(TivoteDb context) : base(context)
        {
        }
        public async Task<IActionResult> Index()
        {
            CurrentUrl.Action = "Index";
            CurrentUrl.Model = await Context.Departments.Include(d => d.Contacts).ThenInclude(c => c.Location).ToListAsync();
            return View(CurrentUrl.Model);
        }
        public async Task<IActionResult> GroupByLocations()
        {
            CurrentUrl.Action = "GroupByLocations";
            CurrentUrl.Model = await Context.Locations.Include(l => l.Contacts).ThenInclude(c => c.Department).ToListAsync();
            return View(CurrentUrl.Model);
        }
        public async Task<IActionResult> GroupByLastName()
        {
            CurrentUrl.Action = "Index";
            CurrentUrl.Model = await Context.Locations.Include(l => l.Contacts).ThenInclude(c => c.Department).ToListAsync();
            return View(CurrentUrl.Model);
        }
        public IActionResult SearchContact()
        {
            SearchContact searchContact = new();
            return View(searchContact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchContact([Bind("SearchText")] SearchContact searchContact)
        {
            if (ModelState.IsValid)
            {
                _ = await Context.Departments.ToListAsync();
                _ = await Context.Locations.ToListAsync();
                searchContact.SearchText = searchContact.SearchText.ToLower();
                IEnumerable<Contact> contacts = await Context.Contacts
                    .Where(c => c.FirstName.ToLower().Contains(searchContact.SearchText) ||
                                c.LastName.ToLower().Contains(searchContact.SearchText) ||
                                c.Email.ToLower().Contains(searchContact.SearchText) ||
                                c.Number.ToLower().Contains(searchContact.SearchText)||
                                c.Department.Name.ToLower().Contains(searchContact.SearchText)||
                                c.Location.Name.ToLower().Contains(searchContact.SearchText))
                    .ToListAsync();
                searchContact.SerchResult.AddRange(contacts);
            }
            return View(searchContact);
        }
        public IActionResult CreateLocation()
        {
            Location location = new();
            ViewBag.Modal = new ModalViewComponent.ModalPath("افزودن محل استقرار", "CreateLocation", location);
            return View(CurrentUrl.Action, CurrentUrl.Model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLocation([Bind("Name,Id")] Location location)
        {
            if (ModelState.IsValid)
            {
                Context.Locations.Add(location);
                await Context.SaveChangesAsync();
                ViewBag.Modal = null;
                return RedirectToAction(CurrentUrl.Action);
            }
            ViewBag.Modal = new ModalViewComponent.ModalPath("افزودن محل استقرار", "CreateLocation", location);
            return View(CurrentUrl.Action, CurrentUrl.Model);
        }
        public IActionResult CreateDepartment()
        {
            Department department = new();
            ViewBag.Modal = new ModalViewComponent.ModalPath("افزودن مجموعه سازمانی", "CreateDepartment", department);
            return View(CurrentUrl.Action, CurrentUrl.Model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDepartment([Bind("Name,Id")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.Id = Guid.NewGuid();
                Context.Departments.Add(department);
                await Context.SaveChangesAsync();
                ViewBag.Modal = null;
                return RedirectToAction(CurrentUrl.Action);
            }
            IEnumerable<Department> departments = await Context.Departments.Include(d => d.Contacts).ThenInclude(c => c.Location).ToListAsync();
            ViewBag.Modal = new ModalViewComponent.ModalPath("افزودن محل استقرار", "CreateDepartment", department);
            return View(CurrentUrl.Action, CurrentUrl.Model);
        }
        public void SetTitles()
        {
            IEnumerable<Title> titles = Context.Titles.ToList();
            SelectListItem[] titlesItems = new SelectListItem[titles.Count()];
            for (int i = 0; i < titles.Count(); i++)
            {
                titlesItems[i] = new SelectListItem { Value = titles.ElementAt(i).Id.ToString(), Text = titles.ElementAt(i).Name };
            }
            ViewData["TitleId"] = titlesItems;
        }
        public void SetDepartments()
        {
            IEnumerable<Department> departments = Context.Departments.ToList();
            SelectListItem[] departmentsItems = new SelectListItem[departments.Count()];
            for (int i = 0; i < departments.Count(); i++)
            {
                departmentsItems[i] = new SelectListItem { Value = departments.ElementAt(i).Id.ToString(), Text = departments.ElementAt(i).Name };
            }
            ViewData["DepartmentId"] = departmentsItems;
        }
        public void SetLocations()
        {
            IEnumerable<Location> locations = Context.Locations.ToList();
            SelectListItem[] locationsItems = new SelectListItem[locations.Count()];
            for (int i = 0; i < locations.Count(); i++)
            {
                locationsItems[i] = new SelectListItem { Value = locations.ElementAt(i).Id.ToString(), Text = locations.ElementAt(i).Name };
            }
            ViewData["LocationId"] = locationsItems;
        }
        

        public async Task<IActionResult> EditContact(Guid id)
        {
            SetTitles();
            SetDepartments();
            SetLocations();
            Contact? contact = await Context.Contacts.Include(c => c.Title).Include(c => c.Department).Include(c => c.Location).FirstOrDefaultAsync(c => c.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            ContactDto contactDto = new()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Number = contact.Number,
                DepartmentId = contact.Department.Id,
                LocationId = contact.Location.Id,
                TitleId = contact.Title.Id
            };
            ViewBag.Modal = new ModalViewComponent.ModalPath("ویرایش اطلاعات تماس", "CreateContact", contactDto);
            IEnumerable<Department> departments = await Context.Departments.Include(d => d.Contacts).ThenInclude(c => c.Location).ToListAsync();
            return View("Index", departments);
        }
        public IActionResult CreateContact()
        {
            SetTitles();
            SetDepartments();
            SetLocations();
            ContactDto contactDto = new();
            ViewBag.Modal = new ModalViewComponent.ModalPath("افزودن اطلاعات تماس", "CreateContact", contactDto);
            return View(CurrentUrl.Action, CurrentUrl.Model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContact(ContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                Department? department = await Context.Departments.FindAsync(contactDto.DepartmentId);
                Location? location = await Context.Locations.FindAsync(contactDto.LocationId);
                Title? title = await Context.Titles.FindAsync(contactDto.TitleId);
                if (department == null || location == null || title == null)
                {
                    return NotFound();
                }
                Contact contact = new()
                {
                    FirstName = contactDto.FirstName,
                    LastName = contactDto.LastName,
                    Number = contactDto.Number,
                    Email = contactDto.Email,
                    Department = department,
                    Location = location,
                    Title = title
                };
                if (contactDto.Id == Guid.Empty)
                {
                    contact.Id = Guid.NewGuid();
                    Context.Contacts.Add(contact);
                }
                else
                {
                    contact.Id = contactDto.Id;
                    Context.Contacts.Update(contact);
                }
                await Context.SaveChangesAsync();
                ViewBag.Modal = null;
                string username = "SJSCO\\" + contact.Email.Split('@')[0];
                User? user = await Context.Users.Include(u => u.Contact).FirstOrDefaultAsync(u => u.Name == username);
                if (user == null)
                {
                    user = new User
                    {
                        Name = username,
                    };
                    user.Contact = contact;
                    Context.Users.Add(user);
                    await Context.SaveChangesAsync();
                }
                else
                {
                    if(user.Contact is null)
                        user.Contact = contact;
                    Context.Users.Update(user);
                    await Context.SaveChangesAsync();
                }
                return RedirectToAction(CurrentUrl.Action);
            }
            SetTitles();
            SetDepartments();
            SetLocations();
            ViewBag.Modal = new ModalViewComponent.ModalPath("افزودن اطلاعات تماس", "CreateContact", contactDto);
            return View(CurrentUrl.Action, CurrentUrl.Model);
        }
    }
}