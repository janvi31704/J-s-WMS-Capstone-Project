using WMS.Application.DTOs;

namespace WMS.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();

        Task<RoleDto> GetByIdAsync(int id);

        Task AddAsync(CreateRoleDto dto);

        Task UpdateAsync(int id, UpdateRoleDto dto);

        Task DeleteAsync(int id);
    }
}