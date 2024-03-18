using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Blog.Api.Tools
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {        
        protected readonly Model.DTO.User User;
        protected readonly IHttpContextAccessor HttpContextAccessor;
        public ApiControllerBase(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
               
                if (httpContextAccessor.HttpContext.Items["User"] != null)
                {
                    User = httpContextAccessor.HttpContext.Items["User"] as Model.DTO.User;
                }

            }
            HttpContextAccessor = httpContextAccessor;

        }
    }
}
