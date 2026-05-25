using WMS.Application.DTOs;

namespace WMS.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();

        Task<DepartmentDto> GetByIdAsync(int id);

        Task AddAsync(CreateDepartmentDto dto);

        Task UpdateAsync(int id, UpdateDepartmentDto dto);

        Task DeleteAsync(int id);
    }
}