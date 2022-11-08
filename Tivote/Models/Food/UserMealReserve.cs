using Tivote.Models.Admin;

namespace Tivote.Models.Food
{
    public class UserMealReserve : Entity
    {
        public User User { get; set; } = default!;
        public MenuItem MenuItem { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}
