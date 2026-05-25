using WMS.Application.DTOs;

namespace WMS.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();

        Task<EmployeeDto> GetByIdAsync(int id);

        Task AddAsync(CreateEmployeeDto dto);

        Task UpdateAsync(int id, UpdateEmployeeDto dto);

        Task DeleteAsync(int id);
    }
}