using Blog.Model.Model;
using Blog.Services.Interface;


namespace Blog.Services
{
    public class PostService : IPostService
    {
        private readonly Data.UnitOfWork.IUnitOfWork unitOfWork;
        public PostService(Data.UnitOfWork.IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<int>> GetPostsCount()
        {
            try
            {
                var count = await unitOfWork.PostRepository.GetCountAsync();
                return new ServiceResponse<int>(count);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<int>(ex);
            }
        }

        public async Task<ServiceResponse<Post>> GetPost(Guid id)
        {
            try
            {
                var post = await unitOfWork.PostRepository.GetOneAsync(p => p.Id == id, p => p.PostRates);
                if(post == null)
                {
                    return new ServiceResponse<Post>("Recorde not found");
                }
                return new ServiceResponse<Post>(post);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Post>(ex);
            }
        }

        public async Task<ServiceResponse<List<Post>>> GetPosts(int take, int skip)
        {
            try
            {
                var posts = await unitOfWork.PostRepository.GetAsync(p => true, p => p.OrderBy(o => o.Title), skip, take, p => p.PostRates);
                if (posts == null)
                {
                    return new ServiceResponse<List<Post>>("Recorde not found");
                }
                return new ServiceResponse<List<Post>>(posts.ToList());
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Post>>(ex);
            }
        }

        public async Task<ServiceResponse<bool>> RatePost(Guid postId, Guid userId, int rate)
        {
            try
            {
                var existingRate = await unitOfWork.PostRateRepository.GetOneAsync(p => p.PostId == postId && p.UserId == userId);
                if(existingRate == null)
                {
                    unitOfWork.PostRateRepository.Create(new PostRate()
                    {
                        Id = Guid.NewGuid(),
                        PostId = postId,
                        UserId = userId,
                        Value = rate
                    });
                    await unitOfWork.SaveAsync();
                }
                else
                {
                    existingRate.Value = rate;
                    unitOfWork.PostRateRepository.Update(existingRate);
                    await unitOfWork.SaveAsync();
                }
                return new ServiceResponse<bool>(true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>(ex);
            }
        }
    }
}
