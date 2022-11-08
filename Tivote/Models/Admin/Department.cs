using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tivote.Models.Admin
{
    public class Department : Entity
    {
        [DisplayName("نام مجموعه سازمانی")]
        [Required(ErrorMessage = "نام مجموعه سازمانی را وارد کنید")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("کارکنان")]
        public List<Contact> Contacts { get; set; } = new();
    }
}

