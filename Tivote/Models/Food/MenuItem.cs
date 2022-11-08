namespace Tivote.Models.Food
{
    public class MenuItem : Entity
    {
        public MealSupplier Supplier { get; set; } = default!;
        public Meal Meal { get; set; } = default!;
    }
}
