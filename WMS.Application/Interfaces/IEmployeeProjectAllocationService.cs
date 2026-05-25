using WMS.Application.DTOs;

namespace WMS.Application.Interfaces
{
    public interface IEmployeeProjectAllocationService
    {
        Task<IEnumerable<EmployeeProjectAllocationDto>> GetAllAsync();

        Task<EmployeeProjectAllocationDto?> GetByIdAsync(int id);

        Task AddAsync(CreateEmployeeProjectAllocationDto dto);

        Task UpdateAsync(int id, UpdateEmployeeProjectAllocationDto dto);

        Task DeleteAsync(int id);
    }
}