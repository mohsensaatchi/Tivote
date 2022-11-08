using Microsoft.EntityFrameworkCore;
using Tivote.Models;
using Tivote.Models.Admin;
using Tivote.Models.News;
using Tivote.Models.SidebarLinks;
using Tivote.Models.Documents;
using Tivote.Models.Food;

namespace Tivote.Data
{
    public class TivoteDb : DbContext
    {
        public TivoteDb(DbContextOptions<TivoteDb> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<LinkCategory> LinkCategories { get; set; } = default!;
        public DbSet<Location> Locations { get; set; } = default!;
        public DbSet<News> News { get; set; } = default!;
        public DbSet<NewsCategory> NewsCategories { get; set; } = default!;
        public DbSet<Permission> Permissions { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<SupplierMenuItem> SupplierMenuItems { get; set; } = default!;
        public DbSet<UsefulLink> UsefulLinks { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<MultipleChoiceSurvey> MultipleChoiceSurveys { get; set; } = default!;
        public DbSet<Choice> Choices { get; set; } = default!;
        public DbSet<Vote> Votes { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Document> Documents { get; set; } = default!;
        public DbSet<MenuItem> MenuItems { get; set; } = default!;
        public DbSet<DailyMenu> DailyMenus { get; set; } = default!;
        public DbSet<Meal> Meals { get; set; } = default!;
        public DbSet<MealSupplier> MealSuppliers { get; set; } = default!;
        public DbSet<UserMealReserve> UserMealReserves { get; set; } = default!;
        public DbSet<Title> Titles { get; set; } = default!;
        //public DbSet<HolyDay> HolyDays { get; set; } = default!;
    }
}
