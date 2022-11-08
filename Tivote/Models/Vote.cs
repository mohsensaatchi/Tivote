using Tivote.Models.Admin;

namespace Tivote.Models
{
    public class Vote : Entity
    {
        public User User { get; set; } = default!;
        public MultipleChoiceSurvey MultipleChoiceSurvey { get; set; } = default!;
        public Choice SelectedChoice { get; set; } = default!;
    }
}
