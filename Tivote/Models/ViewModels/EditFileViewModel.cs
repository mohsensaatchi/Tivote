namespace Tivote.Models.ViewModels
{
    public class EditFileViewModel : UploadFileViewModel
    {
        public Guid Id { get; set; }
        public string ExistingFile { get; set; } = string.Empty;
    }
}
