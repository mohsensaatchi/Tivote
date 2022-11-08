using System;
using System.ComponentModel;

namespace Tivote.Models.Admin
{
    public class Permission : Entity
    {
        [DisplayName("نام مجوز")]
        public string Name { get; set; } = string.Empty;
    }
}

