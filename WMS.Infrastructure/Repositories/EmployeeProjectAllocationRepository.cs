using Microsoft.EntityFrameworkCore;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class EmployeeProjectAllocationRepository
        : IEmployeeProjectAllocationRepository
    {
        private readonly AppDbContext _context;

        public EmployeeProjectAllocationRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeProjectAllocation>>
            GetAllAsync()
        {
            return await _context.EmployeeProjectAllocations
                .ToListAsync();
        }

        public async Task<EmployeeProjectAllocation?>
            GetByIdAsync(int id)
        {
            return await _context.EmployeeProjectAllocations
                .FindAsync(id);
        }

        public async Task AddAsync(
            EmployeeProjectAllocation allocation)
        {
            await _context.EmployeeProjectAllocations
                .AddAsync(allocation);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(
            EmployeeProjectAllocation allocation)
        {
            _context.EmployeeProjectAllocations
                .Update(allocation);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            EmployeeProjectAllocation allocation)
        {
            _context.EmployeeProjectAllocations
                .Remove(allocation);

            await _context.SaveChangesAsync();
        }
    }
}