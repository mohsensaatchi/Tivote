using System.ComponentModel;
using Tivote.Models.Admin;
using Tivote.Models.Food;

namespace Tivote.Models.ViewModels
{
    public class ReserveMealDto
    {
        public User User { get; set; } = default!;
        [DisplayName("منوی روزانه")]
        public Dictionary<DateTime, List<MenuItem>> DailyMenu { get; set; } = new();
        public int Week { get; set; }
    }
}
