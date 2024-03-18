using AutoMapper;
using Microsoft.Extensions.Options;

namespace Blog.Api.Middleware
{

    public class JwtMiddleware
    {
        private readonly ApplicationSettings.Main AppSettings;
        private readonly IMapper mapper;
        private readonly Microsoft.AspNetCore.Http.RequestDelegate _next;
        public JwtMiddleware(IOptions<ApplicationSettings.Main> options, RequestDelegate next, IMapper mapper)
        {
            AppSettings = options.Value;
            _next = next;
            this.mapper = mapper;
        }


        public async Task Invoke(HttpContext ctx, Blog.Services.Interface.IUserService userService)
        {
            var requestHeaders = ctx.Request.Headers["Authorization"];
            ctx.Items["User"] = null;

            var token = requestHeaders.FirstOrDefault()
                ?.Split(" ")
                .Last();

            if (!string.IsNullOrEmpty(token))
            {
                var claims = Tools.JwtManager.VerifyToekn(token, AppSettings.JwtSettings.SecretKey);

                if (claims != null && claims.Any())
                {
                    var userName = claims.FirstOrDefault(p => p.Type == "username")?.Value;

                    if (!string.IsNullOrEmpty(userName))
                    {                        
                        var userResult = await userService.GetUser(userName);

                        if (userResult.HasResult)
                        {
                            var user = userResult.Data;
                            if (user != null && user.Active && user.Token == token && user.TokenExpireDateTime > DateTime.UtcNow)
                            {
                                ctx.Items["User"] = mapper.Map<Model.DTO.User>(user);
                            }
                        }
                    }
                }
            }

            await _next.Invoke(ctx);
        }
    }

    public static class SetJwtExtensions
    {
        public static IApplicationBuilder UseJwtToken(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtMiddleware>();
        }
    }

}
