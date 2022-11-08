using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using Tivote.Models.Food;

namespace Tivote.Models.ViewModels
{
    public class MenuItemDto
    {
        [DisplayName("تامین کننده")]
        public MealSupplier Supplier { get; set; } = default!;
        [DisplayName("غذا")]
        public string MealId { get; set; } = default!;
        [DisplayName("تصویر")]
        public string IamgeUrl { get; set; } = string.Empty;
        [DisplayName("شرح")]
        public string Decription { get; set; } = string.Empty;
        public List<Meal> Meals { get; } = new();
    }
}
