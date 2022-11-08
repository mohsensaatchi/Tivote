using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Tivote.Models.Admin;
using Tivote.Data;

namespace Tivote
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute(string? permission=default)
        {
            Permission = permission;
        }

        public string? Permission { get; } 
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.User.Identity is null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {

                if ((!CurrentUser.Permissions.Where(p => p.Name == Permission).Any()) && Permission is not null)
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}