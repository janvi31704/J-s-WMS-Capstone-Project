using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;

namespace WMS.Application.Services
{
    public class EmployeeProjectAllocationService
        : IEmployeeProjectAllocationService
    {
        private readonly
            IEmployeeProjectAllocationRepository _repository;

        public EmployeeProjectAllocationService(
            IEmployeeProjectAllocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeProjectAllocationDto>>
            GetAllAsync()
        {
            var allocations = await _repository.GetAllAsync();

            return allocations.Select(a =>
                new EmployeeProjectAllocationDto
                {
                    AllocationId = a.AllocationId,
                    EmpId = a.EmpId,
                    ProjectId = a.ProjectId,
                    AssignedOn = a.AssignedOn,
                    CreateDate = a.CreateDate,
                    CreatedBy = a.CreatedBy,
                    Status = a.Status,
                    UpdatedBy = a.UpdatedBy,
                    UpdatedDate = a.UpdatedDate
                });
        }

        public async Task<EmployeeProjectAllocationDto?>
            GetByIdAsync(int id)
        {
            var allocation =
                await _repository.GetByIdAsync(id);

            if (allocation == null)
                return null;

            return new EmployeeProjectAllocationDto
            {
                AllocationId = allocation.AllocationId,
                EmpId = allocation.EmpId,
                ProjectId = allocation.ProjectId,
                AssignedOn = allocation.AssignedOn,
                CreateDate = allocation.CreateDate,
                CreatedBy = allocation.CreatedBy,
                Status = allocation.Status,
                UpdatedBy = allocation.UpdatedBy,
                UpdatedDate = allocation.UpdatedDate
            };
        }

        public async Task AddAsync(
            CreateEmployeeProjectAllocationDto dto)
        {
            var allocation =
                new EmployeeProjectAllocation
                {
                    EmpId = dto.EmpId,
                    ProjectId = dto.ProjectId,
                    AssignedOn = dto.AssignedOn,
                    CreateDate = DateTime.Now,
                    CreatedBy = dto.CreatedBy,
                    Status = true
                };

            await _repository.AddAsync(allocation);
        }

        public async Task UpdateAsync(
            int id,
            UpdateEmployeeProjectAllocationDto dto)
        {
            var allocation =
                await _repository.GetByIdAsync(id);

            if (allocation == null)
                return;

            allocation.Status = dto.Status;
            allocation.UpdatedBy = dto.UpdatedBy;
            allocation.UpdatedDate = DateTime.Now;

            await _repository.UpdateAsync(allocation);
        }

        public async Task DeleteAsync(int id)
        {
            var allocation =
                await _repository.GetByIdAsync(id);

            if (allocation == null)
                return;

            await _repository.DeleteAsync(allocation);
        }
    }
}