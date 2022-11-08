using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tivote.Models.Admin
{
    public class Location : Entity
    {
        [DisplayName("نام مکان استقرار")]
        [Required(ErrorMessage = "نام مکان استقرار را وارد کنید")]
        public string Name { get; set; } = string.Empty;
        public List<Contact> Contacts { get; set; } = new();
    }
}
