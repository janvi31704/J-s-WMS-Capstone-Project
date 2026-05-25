using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectAllocationController
        : ControllerBase
    {
        private readonly
            IEmployeeProjectAllocationService _service;

        public EmployeeProjectAllocationController(
            IEmployeeProjectAllocationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var allocation =
                await _service.GetByIdAsync(id);

            if (allocation == null)
                return NotFound();

            return Ok(allocation);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateEmployeeProjectAllocationDto dto)
        {
            await _service.AddAsync(dto);

            return Ok(
                "Employee assigned to project successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateEmployeeProjectAllocationDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok("Allocation updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok("Allocation deleted successfully");
        }
    }
}