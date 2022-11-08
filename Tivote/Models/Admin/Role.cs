using System;
using System.ComponentModel;

namespace Tivote.Models.Admin
{
    public class Role : Entity
    {
        [DisplayName("نام نقش")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("مجوزها")]
        public List<Permission> Permissions { get; set; } = new();
    }
}

