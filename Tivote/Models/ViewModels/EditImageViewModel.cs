namespace Tivote.Models.ViewModels
{
    public class EditImageViewModel : UploadImageViewModel
    {
        public Guid Id { get; set; }
        public string? ExistingImage { get; set; } = string.Empty;
    }
}
