using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tivote.Models.Documents
{
    public class Document : Entity
    {
        [DisplayName("نام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام فایل را وارد کنید")]
        public string Name { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = true, ErrorMessage = "شرح فایل را وارد کنید")]
        [DisplayName("شرح")]
        public string? Description { get; set; }
        [DisplayName("شاخه")]
        public Category Parent { get; set; } = default!;
        [DisplayName("نسخه")]
        public int Version { get; set; } = 1;
        [DisplayName("مسیر فایل")]
        public string FileUrl { get; set; } = string.Empty;
        [DisplayName("منسوخ")]
        public bool Deprecated { get; set; } = false;
        [DisplayName("مسیر")]
        public string Path => Parent is null ? "/" + $"<a href=/Documents/Index/{Id}>{Name}</a>" : Parent.HtmlPath + "/" + $"<a href=/Documents/Index/{Id}>{Name}</a>";
    }
}
