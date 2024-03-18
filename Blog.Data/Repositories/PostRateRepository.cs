using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;


namespace Blog.Data.Repositories
{
    public interface IPostRateRepository : IRepository<Blog.Model.Model.PostRate>
    {
    }

    public class PostRateRepository : Repository<Blog.Model.Model.PostRate>, IPostRateRepository
    {

        public PostRateRepository(DbContext context) : base(context)
        {

        }
    }
}
