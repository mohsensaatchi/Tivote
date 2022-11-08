using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tivote.Data;
using Tivote.Models;
using Tivote.Models.Admin;
using Tivote.Models.SidebarLinks;

namespace Tivote.Controllers
{
    public class MainController : Controller
    {
        private readonly TivoteDb _context;
        public MainController(TivoteDb context)
        {
            _context = context;
        }
        public async Task<User?> CurrentUserAsync()
        {
            string username = "";
            User user;
            if (HttpContext is null) return null;
            ClaimsPrincipal contextUser = HttpContext.User;
            if (contextUser.Identity is not null)
                username = contextUser.Identity.Name is null ? "" : contextUser.Identity.Name;
            _ = Context.Titles.ToList();
            if (await Context.Users.Where(u => u.Name == username).AnyAsync())
                user = await Context.Users.Include(u => u.Contact).Include(u => u.Roles).ThenInclude(r => r.Permissions).Where(u => u.Name == username).FirstAsync();
            else
            {
                user = new() { Name = username, Id = Guid.NewGuid() };
                Context.Users.Add(user);
                Context.SaveChanges();
            }
            string email = (username.Trim().Split('\\')[1] + "@tivenergy.com").ToLower();
            Contact? contact = await Context.Contacts.Where(c => c.Email == email).FirstOrDefaultAsync();
            if (contact is not null)
            {
                if (user.Contact is null)
                {
                    user.Contact = contact;
                    Context.SaveChanges();
                }
                CurrentUser.Name = $"کاربر: {user.Contact.FirstName} {user.Contact.LastName}";
            }
            else CurrentUser.Name = $"User: {username}";
            CurrentUser.Roles = user.Roles.ToList();
            foreach (Role role in user.Roles)
                foreach (Permission permission in role.Permissions)
                {
                    if (!CurrentUser.Permissions.Contains(permission))
                        CurrentUser.Permissions.Add(permission);
                }
            return user;
        }
        public TivoteDb Context => _context;
        public async Task<IEnumerable<UsefulLink>> UsefulLinks()
        {
            return await _context.UsefulLinks.ToListAsync();
        }
        public async Task<IEnumerable<LinkCategory>> LinkCategories()
        {
            return await _context.LinkCategories.ToListAsync();
        }
        public List<NavbarMenuItem> SidebarMenuItems = new();
        public IActionResult CloseModal()
        {
            ViewBag.Modal = null;
            return RedirectToAction("Index");
        }
    }
}
