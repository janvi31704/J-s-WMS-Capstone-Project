using WMS.Domain.Entities;

namespace WMS.Application.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();

        Task<Role> GetByIdAsync(int id);

        Task AddAsync(Role role);

        Task UpdateAsync(Role role);

        Task DeleteAsync(Role role);
    }
}