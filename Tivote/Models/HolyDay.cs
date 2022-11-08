using System;
using System.ComponentModel;

namespace Tivote.Models
{
    public class HolyDay : Entity
    {
        [DisplayName("تاریخ")]
        public DateTime Date { get; set; }
        [DisplayName("مناسبت")]
        public string Name { get; set; } = string.Empty;
    }
}

