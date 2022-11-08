using System.ComponentModel;

namespace Tivote.Models.Documents
{
    public class Category : Entity
    {
        [DisplayName("نام")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("شرح")]
        public string? Description { get; set; }
        [DisplayName("بالاسری")]
        public Category? Parent { get; set; }
        [DisplayName("فایلها")]
        public List<Document> Files { get; set; } = new();
        [DisplayName("زیرشاخه‌ها")]
        public List<Category> Subcategories { get; } = new();
        [DisplayName("مسیر")]
        public string HtmlPath => Parent is null ? "/" + $"<a href=/Documents/Index/{Id}>{Name}</a>" : Parent.HtmlPath + "/" + $"<a href=/Documents/Index/{Id}>{Name}</a>" ;
        public string Path => Parent is null ? $"/{Name}" : Parent.HtmlPath + $"/{Name}";
    }
}
