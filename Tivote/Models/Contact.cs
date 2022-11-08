using System.ComponentModel;
using Tivote.Models.Admin;

namespace Tivote.Models
{
    public class Contact : Entity
    {
        public Title Title { get; set; } = default!;
        [DisplayName("نام")]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; } = string.Empty;
        [DisplayName("شماره داخلی")]
        public string Number { get; set; } = string.Empty;
        [DisplayName("ایمیل")]
        public string Email { get; set; } = string.Empty;
        [DisplayName("شماره پرسنلی")]
        public string PersonelNumber { get; set; } = string.Empty;
        [DisplayName("محل استقرار")]
        public Location Location { get; set; } = default!;
        [DisplayName("واحد")]
        public Department Department { get; set; } = default!;
    }
}