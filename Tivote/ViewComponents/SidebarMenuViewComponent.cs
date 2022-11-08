using Microsoft.AspNetCore.Mvc;

namespace Tivote.ViewComponents
{
    public class SidebarMenuViewComponent : ViewComponent
    {
        public SidebarMenuViewComponent()
        {
        }
        public List<SidebarMenuItem> MenuItems { get; set; } = new();
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<SidebarMenuItem> menuItems)
        {
            foreach (var item in menuItems)
            {
                MenuItems.Add(item);
            }
            return View("Default", MenuItems);
        }
        public class SidebarMenuItem
        {
            public string Area { get; set; } = string.Empty;
            public string Controller { get; set; } = string.Empty;
            public string Action { get; set; } = string.Empty;
            public string Text { get; set; } = string.Empty;
            public string Target { get; set; } = string.Empty;
            public bool Modal { get; set; }
        }
    }
}
