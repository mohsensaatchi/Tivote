using System.ComponentModel;
using Tivote.Models.Admin;

namespace Tivote.Models.ViewModels
{
    public class ContactDto 
    {
        public Guid Id { get; set; }
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
        [DisplayName("عنوان")]
        public Guid TitleId { get; set; } = default!;
        [DisplayName("محل استقرار")]
        public Guid LocationId { get; set; } = default!;
        [DisplayName("واحد")]
        public Guid DepartmentId { get; set; } = default!;
        [DisplayName("نقشهای کاربری")]
        public List<Role> Roles { get; set; } = new();
    }
}
