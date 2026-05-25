using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers
{
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
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            await _service.AddAsync(dto);

            return Ok("Employee created successfully");
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok("Employee updated successfully");
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok("Employee deleted successfully");
        }
    }
}