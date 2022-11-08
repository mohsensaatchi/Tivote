using Tivote.Models.Admin;

namespace Tivote.Models.ViewModels
{
    public class SearchContact
    {
        public string SearchText { get; set; } = string.Empty;
        public List<Contact> SerchResult { get; set; } = new();
    }
}
