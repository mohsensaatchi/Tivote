using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Tivote.Models.Food;

namespace Tivote.Models
{
    public class SupplierMenuItem : Entity
    {
        [DisplayName("تامین کننده")]
        public MealSupplier Supplier { get; set; } = default!;
        [DisplayName("غذا")]
        public Meal Meal { get; set; } = default!;
        [DisplayName("تصویر")]
        public string IamgeUrl { get; set; } = string.Empty;
        [DisplayName("شرح")]
        public string Decription { get; set; } = string.Empty;
    }
}

