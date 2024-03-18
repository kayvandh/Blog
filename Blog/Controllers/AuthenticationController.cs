using AutoMapper;
using Blog.Api.Tools;
using Blog.Model.DTO;
using Blog.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IUserService userService;
        private readonly Api.ApplicationSettings.Main AppSettings;
        private readonly IMapper mapper;
        public AuthenticationController(IHttpContextAccessor httpContextAccessor, IUserService userService, IOptions<Api.ApplicationSettings.Main> options, IMapper mapper) : base(httpContextAccessor)
        {
            this.userService = userService;
            AppSettings = options.Value;
            this.mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<ApiResult<LoginResponse>> Login(LoginRequest request)
        {
            
            var userResult = await userService.GetUser(request.UserName);

            if (userResult.HasError)
            {
                return new ApiResult<LoginResponse>(System.Net.HttpStatusCode.InternalServerError, userResult.ErrorMessage);
            }

            if (!userResult.HasResult)
            {
                return new ApiResult<LoginResponse>(System.Net.HttpStatusCode.NotFound, userResult.ErrorMessage);
            }

            var checkPassword = Infrastructure.PasswordHasher.Verify(request.Password, userResult.Data.Password);

            if (!checkPassword) return new ApiResult<LoginResponse>(System.Net.HttpStatusCode.NotFound, "Invalid Username or Password");

            var user = mapper.Map<Model.DTO.User>(userResult.Data);

            if (user == null || !user.Active)
            {
                return new ApiResult<LoginResponse>(System.Net.HttpStatusCode.NotFound, "Invalid Username or Password");
            }

            var token = JwtManager.GenerateToken(user, AppSettings);

            if (token == null)
            {
                return new ApiResult<LoginResponse>(System.Net.HttpStatusCode.InternalServerError);
            }

            var userEntity = userResult.Data;
            userEntity.Token = token.Token;            
            userEntity.TokenExpireDateTime = token.ExpireDateTime;
            userEntity.LastLoginDateTime = DateTime.UtcNow;            

            var updateResult = await userService.UpdateUser(userEntity);
            if (updateResult.HasError)
            {
                return new ApiResult<LoginResponse>(System.Net.HttpStatusCode.InternalServerError, updateResult.ErrorMessage);
            }

            return new ApiResult<LoginResponse>(new LoginResponse() { User = user, TokenDetail = token });

        }
    }
}
