using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tivote.Data;
using Tivote.Models.News;

namespace Tivote.ViewComponents
{
    public class NewsCardViewComponent : ViewComponent
    {
        TivoteDb db;

        public NewsCardViewComponent(TivoteDb context) => db = context;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<News> news = await db.News.OrderByDescending(n => n.DateTime).ToListAsync();
            return View(news);
        }
    }
}
