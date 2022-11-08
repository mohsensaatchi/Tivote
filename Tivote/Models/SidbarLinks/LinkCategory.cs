using System.ComponentModel;

namespace Tivote.Models.SidebarLinks
{
    public class LinkCategory : Entity
    {
        [DisplayName("گروه")]
        public string Name { get; set; } = string.Empty;
    }
}
