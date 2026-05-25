using WMS.Domain.Entities;

namespace WMS.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserLogin?> GetUserByUsernameAsync(string username);

        Task AddUserAsync(UserLogin user);
    }
}