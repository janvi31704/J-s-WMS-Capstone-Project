using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        // GET: api/attendance
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attendance = await _service.GetAllAsync();

            return Ok(attendance);
        }

        // GET: api/attendance/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attendance = await _service.GetByIdAsync(id);

            if (attendance == null)
                return NotFound();

            return Ok(attendance);
        }

        // POST: api/attendance/checkin
        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn(CheckInDto dto)
        {
            try
            {
                await _service.CheckInAsync(dto);

                return Ok("Check-in successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/attendance/checkout
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut(CheckOutDto dto)
        {
            try
            {
                await _service.CheckOutAsync(dto);

                return Ok("Check-out successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("monthly/{employeeId}")]
        public async Task<IActionResult> GetMonthlyAttendance(
            int employeeId,
            int month,
            int year)
        {
            var result =
                await _service.GetMonthlyAttendanceAsync(
                    employeeId,
                    month,
                    year);

            return Ok(result);
        }

        [Authorize]

        [HttpGet("report")]
        public async Task<IActionResult>
            GetAttendanceReport()
        {
            return Ok(
                await _service.GetAttendanceReportAsync());
        }
    }
}