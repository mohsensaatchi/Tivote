using System;
using System.ComponentModel;

namespace Tivote.Models.Food
{
    public class MealSupplier : Entity
    {
        [DisplayName("نام تامین کننده")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("منو")]
        public List<SupplierMenuItem> MenuItems { get; set; } = new();
    }
}

