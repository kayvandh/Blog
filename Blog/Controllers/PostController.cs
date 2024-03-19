using AutoMapper;
using Blog.Api.Tools;
using Blog.Model.DTO;
using Blog.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ApiControllerBase
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;
        public PostController(IHttpContextAccessor httpContextAccessor, IPostService postService, IMapper mapper) : base(httpContextAccessor)
        {
            this.postService = postService;
            this.mapper = mapper;
        }

        [HttpGet]        
        public async Task<ApiResult<Model.DTO.GetPostsResponse>> GetPosts([FromQuery]Model.DTO.GetPostsRequest request)
        {
            var postsResult = await postService.GetPosts(request.TakeCount,request.Skip);

            if (postsResult.HasError)
            {
                return new ApiResult<Model.DTO.GetPostsResponse>(System.Net.HttpStatusCode.InternalServerError, postsResult.ErrorMessage);
            }

            if (!postsResult.HasResult)
            {
                return new ApiResult<Model.DTO.GetPostsResponse>(System.Net.HttpStatusCode.NotFound, postsResult.ErrorMessage);
            }
            var postsCount = await postService.GetPostsCount();

            return new ApiResult<GetPostsResponse>(new GetPostsResponse()
            {
                Posts = postsResult.Data.Select(mapper.Map<Model.DTO.Post>).ToList(),
                TotalRowCount = postsCount.Data
            });
        }

        [HttpPost("Score")]
        [Tools.Authorize]
        public async Task<ApiResult<bool>> Score(RatePostRequest request)
        {
            var result = await postService.RatePost(request.PostId, request.UserId, request.Value);

            if(result.HasError)
            {
                return new ApiResult<bool>(System.Net.HttpStatusCode.InternalServerError,result.ErrorMessage);
            }

            return new ApiResult<bool>(result.Data);
        }
    }
}
