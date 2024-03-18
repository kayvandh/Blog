using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Repository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(dbContext);
        }

        public virtual int Save()
        {
            var result = dbContext.SaveChanges();
            return result;
        }

        public virtual async Task<int> SaveAsync()
        {
            var result = await dbContext.SaveChangesAsync();
            return result;
        }

        public bool IsDisposed { get; protected set; }


        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                }
            }

            IsDisposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }


    }
}
