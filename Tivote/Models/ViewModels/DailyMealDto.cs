using System.ComponentModel;

namespace Tivote.Models.ViewModels
{
    public class DailyMealDto
    {
        public Guid Id { get; set; }
        [DisplayName("تاریخ")]
        public DateTime Date { get; set; } = DateTime.Now;
        [DisplayName("غذاهای روز")]
        public List<string> Meals { get; set; } = new();
    }
}
