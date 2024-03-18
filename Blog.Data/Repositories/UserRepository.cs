using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;


namespace Blog.Data.Repositories
{
    public interface IUserRepository : IRepository<Blog.Model.Model.User>
    {
    }

    public class UserRepository : Repository<Blog.Model.Model.User>, IUserRepository
    {

        public UserRepository(DbContext context) : base(context)
        {

        }
    }
}
