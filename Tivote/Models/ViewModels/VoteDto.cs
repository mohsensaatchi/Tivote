namespace Tivote.Models.ViewModels
{
    public class VoteDto
    {
        public string Question { get; set; } = string.Empty;
        public List<Choice> Choices { get; set; } = new();
        public Guid UserId { get; set; }
        public Guid MultipleChoiceSurveyId { get; set; }
        public Guid SelectedChoice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
