using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : System.IDisposable
    {
        bool IsDisposed { get; }
        int Save();
        Task<int> SaveAsync();

        Repository<T> GetRepository<T>() where T : class;
    }
}
