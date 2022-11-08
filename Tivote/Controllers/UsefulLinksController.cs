using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tivote.Data;
using Tivote.Models.Admin;
using Tivote.Models.SidebarLinks;

namespace Tivote.Controllers
{
    public class UsefulLinksController : MainController
    {
        public UsefulLinksController(TivoteDb context) : base(context)
        {
        }
        public async Task<IActionResult> Index()
        {
            return View(await Context.LinkCategories.ToListAsync());
        }
        public async Task<IActionResult> CreateEditCategory(Guid? id)
        {
            LinkCategory? category;
            if (id == null)
            {
                category = new();
            }
            else
            {
                category = await Context.LinkCategories.FindAsync(id);
                if (category is null) return NotFound();
                
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEditCategory(LinkCategory category)
        {
            if (ModelState.IsValid)
            {
                Context.LinkCategories.Add(category);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public IActionResult CreateEditLink(Guid id)
        {
            return View();
        }
    }
}