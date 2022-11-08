using System;
using System.ComponentModel;

namespace Tivote.Models.Admin
{
    public class User : Entity
    {
        [DisplayName("نام کاربری")]
        public string Name { get; set; } = string.Empty;
        public Contact? Contact { get; set; } = default!;
        public IEnumerable<Role> Roles { get; set; } = default!;
    }
}