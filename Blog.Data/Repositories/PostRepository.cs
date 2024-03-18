using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;


namespace Blog.Data.Repositories
{
    public interface IPostRepository : IRepository<Blog.Model.Model.Post>
    {
    }

    public class PostRepository : Repository<Blog.Model.Model.Post>, IPostRepository
    {

        public PostRepository(DbContext context) : base(context)
        {

        }
    }
}
