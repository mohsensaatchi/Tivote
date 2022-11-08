using System.ComponentModel;

namespace Tivote.Models.News
{
    public class NewsCategory : Entity
    {
        [DisplayName("نام")]
        public string Name { get; set; } = string.Empty;
        public List<News> News { get; set; } = new();
    }
}
