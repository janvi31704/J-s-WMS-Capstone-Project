using WMS.Application.DTOs;

namespace WMS.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync();

        Task<IEnumerable<AttendanceChartDto>>GetAttendanceChartAsync();

         Task<LeaveStatisticsDto>GetLeaveStatisticsAsync();

        Task<ProjectStatisticsDto>GetProjectStatisticsAsync();
    }
}