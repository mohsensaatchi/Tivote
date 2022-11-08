using System;
using Tivote.Models.Admin;
using Tivote.Models.Food;

namespace Tivote.Models.ViewModels
{
    public class WeeklyUserMeal
    {
        public WeeklyUserMeal()
        {
        }
        public User User { get; set; } = default!;
        public List<DailyMenu> DailyMeals { get; set; } = new();
    }
}

