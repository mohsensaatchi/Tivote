using Tivote.Models.Documents;

namespace Tivote.Models.ViewModels
{
    public class DocumentDto : EditFileViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid ParentId { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}
