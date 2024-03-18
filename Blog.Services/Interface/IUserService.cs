using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Interface
{
    public interface IUserService
    {
        Task<ServiceResponse<Model.Model.User>> GetUser(string username);
        Task<ServiceResponse<bool>> UpdateUser(Model.Model.User user);
    }
}
