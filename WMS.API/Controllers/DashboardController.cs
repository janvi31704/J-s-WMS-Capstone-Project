using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(
            IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            return Ok(await _service.GetSummaryAsync());
        }

        [HttpGet("attendance-chart")]
        public async Task<IActionResult>
            GetAttendanceChart()
        {
            return Ok(
                await _service.GetAttendanceChartAsync());
        }

        [HttpGet("leave-statistics")]
        public async Task<IActionResult>
            GetLeaveStatistics()
        {
            return Ok(
                await _service.GetLeaveStatisticsAsync());
        }

        [HttpGet("project-statistics")]
        public async Task<IActionResult>
            GetProjectStatistics()
        {
            return Ok(
                await _service.GetProjectStatisticsAsync());
        }
    }
}