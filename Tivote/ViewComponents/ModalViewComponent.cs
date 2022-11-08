using System;
using Microsoft.AspNetCore.Mvc;

namespace TivPortal.ViewComponents
{
    public class ModalViewComponent : ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync(string title,string action,object model)
        {
            ModalPath path = new(title,action,model);
            return View(path);
        }
        public class ModalPath
        {
            public ModalPath(string title,string action,object model)
            {
                Title = title;
                Action = action;
                Model = model;
            }
            public string Title { get; set; } = string.Empty;
            public string Action { get; set; } = string.Empty;
            public object Model { get; set; } = default!;
        }
    }
}