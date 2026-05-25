using WMS.Domain.Entities;

namespace WMS.Application.Interfaces
{
    public interface IEmployeeProjectAllocationRepository
    {
        Task<IEnumerable<EmployeeProjectAllocation>> GetAllAsync();

        Task<EmployeeProjectAllocation?> GetByIdAsync(int id);

        Task AddAsync(EmployeeProjectAllocation allocation);

        Task UpdateAsync(EmployeeProjectAllocation allocation);

        Task DeleteAsync(EmployeeProjectAllocation allocation);
    }
}