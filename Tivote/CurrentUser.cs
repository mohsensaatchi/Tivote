using System.Globalization;
using Tivote.Models.Admin;
using Tivote.Models.News;

namespace Tivote
{
    public static class CurrentUser 
    {
        private static Dictionary<string, string> days = new()
    {
        {"Monday", "دوشنبه"},
        {"Tuesday", "سه‌شنبه"},
        {"Wednesday", "چهارشنبه"},
        {"Thursday", "پنج‌شنبه"},
        {"Friday", "جمعه"},
        {"Saturday", "شنبه"},
        {"Sunday", "یکشنبه"}
    };
        public static string[] months = new string[12] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        public static string? Name { get; set; } = string.Empty;
        public static List<Role> Roles { get; set; } = new();
        public static List<Permission> Permissions { get; set; } = new();
        public static string Today
        {
            get
            {
                PersianCalendar pc = new();
                return $"{days[@pc.GetDayOfWeek(DateTime.Now).ToString()]} {pc.GetDayOfMonth(DateTime.Now)} {months[pc.GetMonth(DateTime.Now)-1]} {pc.GetYear(DateTime.Now)} {DateTime.Now.Second}";
            }
        }
    }
}
