using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tivote.Data;
using Tivote.Models.Admin;
using Tivote.Models.Food;
using Tivote.Models.ViewModels;

namespace Tivote.Controllers
{
    public class MealController : MainController
    {
        public MealController(TivoteDb context) : base(context)
        {
        }
        public async Task<IActionResult> Index(int wn)
        {
            User? currentUser = await CurrentUserAsync();
            if (currentUser is null)
                return RedirectToAction("Index", "Home");
            ReserveMealDto reserveMealDto = new();
            reserveMealDto.User = currentUser;
            int diff = (7 + (DateTime.Now.DayOfWeek - DayOfWeek.Saturday)) % 7;
            reserveMealDto.Week = wn;
            DateTime weekStart = DateTime.Now.AddDays(-1 * diff).Date;
            weekStart = weekStart.AddDays(7 * wn);
            for (int i = 0; i < 5; i++)
            {
                DateTime day = weekStart.AddDays(i);
                reserveMealDto.DailyMenu.Add(weekStart.AddDays(i), new());
                foreach (var item in Context.DailyMenus.Where(m => m.Date.Year == day.Year && m.Date.Month == day.Month && m.Date.Day == day.Day))
                {
                    reserveMealDto.DailyMenu[day].Add(item.MenuItem);
                }
            }
            return View(reserveMealDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserveMeal(ReserveMealDto reserveMealDto)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in reserveMealDto.DailyMenu)
                {
                    foreach (var menuItem in item.Value)
                    {
                        int a = 100;
                    }
                }
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", reserveMealDto);
        }
        //public IActionResult ReserveMeal(int wn)
        //{
        //    ReserveMealDto reserveMealDto = new();
        //    //DateTime weekStart = DateTime.Now.AddDays(-1 * ((7 + (DateTime.Now.DayOfWeek - DayOfWeek.Saturday)) % 7)).AddDays(7 * wn);
        //    //for (int i=0;i<5;i++)
        //    //{
        //    //    reserveMealDto.DailyMenu.Add(weekStart.AddDays(i), DailyMenus().Where(dm => dm.Date == weekStart.AddDays(i)).Select(dm => dm.MenuItem).ToList());
        //    //}
        //    return View(reserveMealDto);
        //}
        public IActionResult CreateDailyMenu()
        {
            return View();
        }
        public IActionResult WeeklyReport()
        {
            return View();
        }
        public IActionResult AddSupplier()
        {
            return View();
        }
        //public async Task<IActionResult> Suppliers()
        //{
        //    //List<MealSupplier> suppliers = await Context.MealSuppliers.ToListAsync();
        //    //return View(suppliers);
        //}
        public IActionResult CreateSupplier()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSupplier([Bind("Name,Id")] MealSupplier mealSupplier)
        {
            if (ModelState.IsValid)
            {
                mealSupplier.Id = Guid.NewGuid();
                Context.MealSuppliers.Add(mealSupplier);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Suppliers));
            }
            return PartialView(mealSupplier);
        }
        public IActionResult CreateMeal()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMeal([Bind("Name,Id")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                meal.Id = Guid.NewGuid();
                Context.Meals.Add(meal);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Meals));
            }
            return PartialView(meal);
        }
        public async Task<IActionResult> Meals()
        {
            return View(await Context.Meals.ToListAsync());
        }
        public async Task<IActionResult> Suppliers()
        {
            return View(await Context.MealSuppliers.ToListAsync());
        }
    }
}


