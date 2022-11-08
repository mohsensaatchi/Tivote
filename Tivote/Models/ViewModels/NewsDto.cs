using System.ComponentModel;
using Tivote.Models.News;

namespace Tivote.Models.ViewModels
{
    public class NewsDto : EditImageViewModel
    {
        [DisplayName("عنوان")]
        public string Title { get; set; } = string.Empty;
        [DisplayName("شرح مختصر")]
        public string Description { get; set; } = string.Empty;
        [DisplayName("محتوا")]
        public string Content { get; set; } = string.Empty;
        [DisplayName("دسته‌بندی")]
        public Guid CategoryId { get; set; } = default!;
        [DisplayName("زمان انتشار")]
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
