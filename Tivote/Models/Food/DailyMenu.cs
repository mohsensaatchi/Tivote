using System;
using System.ComponentModel;

namespace Tivote.Models.Food
{
    public class DailyMenu : Entity
    {
        [DisplayName("تاریخ")]
        public DateTime Date { get; set; } = DateTime.Now;
        [DisplayName("غذای روز")]
        public MenuItem MenuItem { get; set; } = new();
    }
}

