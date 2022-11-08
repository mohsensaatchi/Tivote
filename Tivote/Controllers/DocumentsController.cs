using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tivote.Data;
using Tivote.Models.Admin;
using Tivote.Models.Documents;
using Tivote.Models.ViewModels;

namespace Tivote.Controllers
{
    public class DocumentsController : MainController
    {
        public DocumentsController(TivoteDb context) : base(context)
        {
        }
        private async void CheckRoot()
        {
            if (!Context.Categories.Where(c => c.Parent == null).Any())
            {
                Category category = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "ریشه",
                    Parent = null
                };
                Context.Categories.Add(category);
                await Context.SaveChangesAsync();
            }
        }
        private async Task<Guid> GetRootId()
        {
            CheckRoot();
            return await Context.Categories.Where(c => c.Parent == null).Select(c => c.Id).FirstOrDefaultAsync();
        }
        public async Task<IActionResult> Index(Guid id)
        {
            User? currentUser = await CurrentUserAsync();
            Category? category;
            if (id == Guid.Empty)
                id = await GetRootId();
            category = await Context.Categories.Include(c => c.Subcategories).Include(c => c.Files).FirstOrDefaultAsync(c => c.Id == id);
            _ = await Context.Categories.Include(c => c.Subcategories).Include(c => c.Files).ToListAsync();
            return View(category);
        }
        public string GetPath(Guid id)
        {
            Category? category = Context.Categories.Include(c => c.Parent).FirstOrDefault(c => c.Id == id);
            if (category is null)
                return "";
            if (category.Parent is null)
                return $"/{category.Name}";
            return GetPath(category.Parent.Id) + $"/{category.Name}";
        }
        public async Task<IActionResult> CreateCategory(Guid? parentId)
        {
            Category? parent = await Context.Categories.FindAsync(parentId);
            if (parent is null)
                return BadRequest();
            CategoryDto categoryDto = new();
            categoryDto.ParentId = parent.Id;
            _ = await Context.Categories.ToListAsync();
            categoryDto.Path = GetPath(parent.Id);
            return View(categoryDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            Category category = new();
            if (ModelState.IsValid)
            {
                Category? parent = await Context.Categories.Include(c => c.Subcategories).FirstOrDefaultAsync(c => c.Id == categoryDto.ParentId);
                if (parent is null) return BadRequest();
                if(parent.Subcategories.Where(s=>s.Name==categoryDto.Name).Any())
                    return View(categoryDto);
                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;
                category.Parent = await Context.Categories.FindAsync(categoryDto.ParentId);
                Context.Categories.Add(category);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = categoryDto.ParentId });
            }
            return View(categoryDto);
        }
        public async Task<IActionResult> UploadDocument(Guid? parentId)
        {
            Category? parent = await Context.Categories.FindAsync(parentId);
            if (parent is null)
                return BadRequest();
            DocumentDto documentDto = new();
            documentDto.ParentId = parent.Id;
            documentDto.Path = GetPath(parent.Id);
            return View(documentDto);
        }
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            Category? category = await Context.Categories.Include(c => c.Parent).Include(c => c.Subcategories).Include(c => c.Files).FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
                return NotFound();
            if (category.Subcategories.Count > 0 || category.Files.Count > 0)
                return BadRequest();
            Context.Categories.Remove(category);
            await Context.SaveChangesAsync();
            return View("Index", category.Parent.Id);
        }
        public async Task<IActionResult> RenameCategory(Guid id)
        {
            return View();
        }
        public async Task<IActionResult> UpdateDocument(Guid id)
        {
            return View();
        }
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadDocument(DocumentDto documentDto)
        {
            if (ModelState.IsValid)
            {
                Category? parent = await Context.Categories.Include(c => c.Subcategories).FirstOrDefaultAsync(c => c.Id == documentDto.ParentId);
                if (parent is null)
                    return BadRequest();
                else
                {
                    string uniqueFileName = ProcessUploadedFile(documentDto);
                    Document document = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = documentDto.Name,
                        Description = documentDto.Description,
                        FileUrl = Path.Combine(uniqueFileName)
                    };
                    if (parent is null) return BadRequest();
                    document.Parent = parent;
                    Context.Documents.Add(document);
                    await Context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { id = documentDto.ParentId });
                }
            }
            return View(documentDto);
        }
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var document = await Context.Documents.FindAsync(id);
            if (document is null) return NotFound();
            if (document.FileUrl == null) return NotFound();
            return File(document.FileUrl, "application/octet-stream", Path.GetFileName(document.FileUrl));
        }
        private static string ProcessUploadedFile(DocumentDto model)
        {
            string filePath = "";
            string result = "";
            string path = Path.Combine("wwwroot", "Uploads", "documents");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (model.File != null)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                filePath = Path.Combine("wwwroot", "Uploads", "documents", uniqueFileName);
                result = Path.Combine("Uploads", "documents", uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.File.CopyTo(fileStream);
                }
            }
            return result;
        }
        public class TestModel
        {
            public bool ShowDialog { get; set; }
        }
        public ActionResult Test()
        {
            TestModel model = new TestModel();

            if (true)
            {
                model.ShowDialog = true;
            }

            return PartialView("Test", model);
        }
    }
}
