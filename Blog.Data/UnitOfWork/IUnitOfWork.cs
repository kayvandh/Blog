using Blog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitOfWork
{
    public interface IUnitOfWork : Infrastructure.UnitOfWork.IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IPostRepository PostRepository { get; }
        IPostRateRepository PostRateRepository { get; }

    }
}
