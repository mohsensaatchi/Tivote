using System.ComponentModel;

namespace Tivote.Models
{
    public class MultipleChoiceSurvey : Entity
    {
        [DisplayName("سوال")]
        public string Question { get; set; } = string.Empty;
        [DisplayName("گزینه ها")]
        public List<Choice> Choices { get; } = new();
        [DisplayName("تصویر")]
        public string ImageUrl { get; set; } = string.Empty;
    }
}