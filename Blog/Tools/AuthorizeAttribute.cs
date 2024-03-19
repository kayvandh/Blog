using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Tools
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute() : base()
        {

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Model.DTO.User? user = context.HttpContext.Items["User"] as Model.DTO.User;

            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }
        }
    }
}
