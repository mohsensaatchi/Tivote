using System.ComponentModel;
using Tivote.Models.Documents;

namespace Tivote.Models.ViewModels
{
    public class CategoryDto
    {
        public Guid ParentId { get; set; }
        [DisplayName("نام")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("شرح")]
        public string? Description { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}
