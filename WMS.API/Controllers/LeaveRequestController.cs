using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _service;

        public LeaveRequestController(ILeaveRequestService service)
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
            var leave = await _service.GetByIdAsync(id);

            if (leave == null)
                return NotFound();

            return Ok(leave);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyLeave(CreateLeaveRequestDto dto)
        {
            try
            {
                await _service.ApplyLeaveAsync(dto);

                return Ok("Leave applied successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveLeave(int id, LeaveApprovalDto dto)
        {
            try
            {
                await _service.ApproveLeaveAsync(id, dto);

                return Ok("Leave approved successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectLeave(int id, LeaveApprovalDto dto)
        {
            try
            {
                await _service.RejectLeaveAsync(id, dto);

                return Ok("Leave rejected successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}