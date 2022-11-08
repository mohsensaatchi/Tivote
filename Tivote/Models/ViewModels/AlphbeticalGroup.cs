using Tivote.Models.Admin;

namespace Tivote.Models.ViewModels
{
    public class AlphbeticalGroup
    {
        public string FirstLetter { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<User> Users { get; set; } = new();
    }
}
