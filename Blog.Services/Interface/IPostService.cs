namespace Blog.Services.Interface
{
    public interface IPostService
    {
        Task<ServiceResponse<List<Model.Model.Post>>> GetPosts(int take,int skip);
        Task<ServiceResponse<Model.Model.Post>> GetPost(Guid id);
        Task<ServiceResponse<int>> GetPostsCount();
        Task<ServiceResponse<bool>> RatePost(Guid postId, Guid userId,int rate);        
    }
}
