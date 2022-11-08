using System.ComponentModel;

namespace Tivote.Models.News
{
    public class News : Entity
    {
        [DisplayName("عنوان")]
        public string Title { get; set; } = string.Empty;
        [DisplayName("شرح مختصر")]
        public string Description { get; set; } = string.Empty;
        [DisplayName("محتوا")]
        public string Content { get; set; } = string.Empty;
        [DisplayName("تصویر")]
        public string? ImageUrl { get; set; } = string.Empty;
        [DisplayName("دسته‌بندی")]
        public NewsCategory Category { get; set; } = default!;
        [DisplayName("زمان انتشار")]
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
