using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;

namespace WMS.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        private readonly IAuditLogService _auditService;

        public EmployeeService(IEmployeeRepository repository, IAuditLogService auditService)
        {
            _repository = repository;
            _auditService = auditService;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();

            return employees.Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Gender = e.Gender,
                DOB = e.DOB,
                DOJ = e.DOJ,
                DepartmentId = e.DepartmentId,
                RoleId = e.RoleId,
                Status = e.Status
            });
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
                return null;

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                DOB = employee.DOB,
                DOJ = employee.DOJ,
                DepartmentId = employee.DepartmentId,
                RoleId = employee.RoleId,
                Status = employee.Status
            };
        }

        public async Task AddAsync(CreateEmployeeDto dto)
        {
            // Business Validation
            if (DateTime.Now.Year - dto.DOB.Year < 18)
            {
                throw new Exception("Employee must be at least 18 years old");
            }

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                DOB = dto.DOB,
                DOJ = dto.DOJ,
                DepartmentId = dto.DepartmentId,
                RoleId = dto.RoleId,
                Status = "Active"
            };

            await _repository.AddAsync(employee);
            await _auditService.AddAsync(
    "Employee",
    employee.EmployeeId,
    "Insert",
    1);
        }

        public async Task UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
                return;

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.Gender = dto.Gender;
            employee.DOB = dto.DOB;
            employee.DOJ = dto.DOJ;
            employee.DepartmentId = dto.DepartmentId;
            employee.RoleId = dto.RoleId;
            employee.UpdatedOn = DateTime.Now;

            await _repository.UpdateAsync(employee);
            await _auditService.AddAsync(
                "Employee",
                employee.EmployeeId,
                "Update",
                1);
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
                return;

            await _repository.DeleteAsync(employee);
            await _auditService.AddAsync(
                "Employee",
                employee.EmployeeId,
                "Delete",
                1);
        }
    }
}