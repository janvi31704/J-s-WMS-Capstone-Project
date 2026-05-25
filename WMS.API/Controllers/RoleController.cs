using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        // GET: api/role
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _service.GetAllAsync();

            return Ok(roles);
        }

        // GET: api/role/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _service.GetByIdAsync(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        // POST: api/role
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDto dto)
        {
            await _service.AddAsync(dto);

            return Ok("Role created successfully");
        }

        // PUT: api/role/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRoleDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok("Role updated successfully");
        }

        // DELETE: api/role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok("Role deleted successfully");
        }
    }
}