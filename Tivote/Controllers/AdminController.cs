using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tivote.Data;
using Tivote.Models.Admin;
using TivPortal.ViewComponents;

namespace Tivote.Controllers
{
    public class AdminController : MainController
    {
        public AdminController(TivoteDb context) : base(context)
        {
            
        }
        public async Task<IActionResult> Index()
        {
            return View(await Context.Users.Include(u=>u.Contact).ToListAsync());
        }
        public async Task<IActionResult> Roles()
        {
            return View(await Context.Roles.ToListAsync());
        }
        public async Task<IActionResult> EditRoles(Guid userId)
        {
            User? user = Context.Users.Include(u => u.Roles).FirstOrDefault();
            if (user is null) return NotFound();
            ViewBag.Modal = new ModalViewComponent.ModalPath("دسترسیهای کاربر", "EditRoles", user);
            ViewBag.Users = await Context.Users.ToListAsync();
            ViewBag.Roles = await Context.Roles.ToListAsync();
            ViewBag.Permissions = await Context.Permissions.ToListAsync();
            return View("Index", await Context.Users.ToListAsync());
        }
        public async Task<IActionResult> Permissions()
        {
            return View(await Context.Permissions.ToListAsync());
        }
        public IActionResult CreatePermission()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePermission([Bind("Name,Id")] Permission permission)
        {

            if (ModelState.IsValid)
            {
                permission.Id = Guid.NewGuid();
                Context.Permissions.Add(permission);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permission);
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole([Bind("Name,Id")] Role role)
        {

            if (ModelState.IsValid)
            {
                role.Id = Guid.NewGuid();
                Context.Roles.Add(role);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
    }
}
