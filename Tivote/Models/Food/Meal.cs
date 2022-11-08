using System;
using System.ComponentModel;

namespace Tivote.Models.Food
{
    public class Meal : Entity
    {
        [DisplayName("نام غذا")]
        public string Name { get; set; } = string.Empty;
        //[DisplayName("تامین کننده")]
        //public MealSupplier Supplier { get; set; } = default!;
    }
}

