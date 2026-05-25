using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        // GET: api/department
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _service.GetAllAsync();

            return Ok(departments);
        }

        // GET: api/department/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _service.GetByIdAsync(id);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        // POST: api/department
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto dto)
        {
            await _service.AddAsync(dto);

            return Ok("Department created successfully");
        }

        // PUT: api/department/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDepartmentDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok("Department updated successfully");
        }

        // DELETE: api/department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok("Department deleted successfully");
        }
    }
}