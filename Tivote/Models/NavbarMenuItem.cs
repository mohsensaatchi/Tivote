namespace Tivote.Models
{
    public class NavbarMenuItem
    {
        public NavbarMenuItem(string text, string area, string controller, string action)
        {
            Text = text;
            Area = area;
            Controller = controller;
            Action = action;
        }
        public string Text { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
    }
}
