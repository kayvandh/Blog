using Blog.Model.Model;
using Blog.Services.Interface;

namespace Blog.Services
{
    public class UserService : IUserService
    {
        private readonly Data.UnitOfWork.IUnitOfWork _unitOfWork;
        public UserService(Data.UnitOfWork.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async  Task<ServiceResponse<User>> GetUser(string username)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetOneAsync(p => p.UserName.ToLower() == username.ToLower());
                return new ServiceResponse<User>(user);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<User>(ex);
            }
        }

        public async Task<ServiceResponse<bool>> UpdateUser(User user)
        {
            try
            {
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return new ServiceResponse<bool>(true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>(ex);
            };
        }
    }
}