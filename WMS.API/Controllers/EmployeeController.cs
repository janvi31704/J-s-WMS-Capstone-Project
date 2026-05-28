using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAllAsync();

            return Ok(employees);
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/employee
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            await _service.AddAsync(dto);

            return Ok(new
{
    message = "Employee added successfully"
});
        }

        // PUT: api/employee/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok(new
{
    message = "Employee updated successfully"
});
        }

        // DELETE: api/employee/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok(new
{
    message = "Employee deleted successfully"
});
        }
    }
}