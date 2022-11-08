using System.ComponentModel;

namespace Tivote.Models.SidebarLinks
{
    public class UsefulLink : Entity
    {
        [DisplayName("نام")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("نشانی")]
        public string Url { get; set; } = string.Empty;
        public LinkCategory Category { get; set; } = default!;
    }
}