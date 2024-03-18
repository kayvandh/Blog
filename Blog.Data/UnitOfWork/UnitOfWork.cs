using Blog.Data.Context;
using Blog.Data.Repositories;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitOfWork
{
    public class UnitOfWork : Infrastructure.UnitOfWork.UnitOfWork ,Blog.Data.UnitOfWork.IUnitOfWork
    {
        public UnitOfWork(BlogContext dbContext) : base(dbContext)
        {
        }


        private IUserRepository userRepository;
        public IUserRepository UserRepository => userRepository ??= new UserRepository(dbContext);

        private IPostRepository postRepository;
        public IPostRepository PostRepository => postRepository ??= new PostRepository(dbContext);

        private IPostRateRepository postRateRepository;
        public IPostRateRepository PostRateRepository => postRateRepository ??= new PostRateRepository(dbContext); 

    }
}
