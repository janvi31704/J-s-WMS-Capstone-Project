using Microsoft.EntityFrameworkCore;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto>
            GetSummaryAsync()
        {
            var today = DateTime.Today;

            return new DashboardSummaryDto
            {
                TotalEmployees =
                    await _context.Employees.CountAsync(),

                TotalDepartments =
                    await _context.Departments.CountAsync(),

                TotalProjects =
                    await _context.Projects.CountAsync(),

                PendingLeaves =
                    await _context.LeaveRequests
                        .CountAsync(l => l.Status == "Pending"),

                PresentEmployeesToday =
                    await _context.Attendances
                        .CountAsync(a =>
                            a.AttendanceDate.Date == today &&
                            a.Status == "Present")
            };
        }
        public async Task<IEnumerable<AttendanceChartDto>>
    GetAttendanceChartAsync()
        {
            return await _context.Attendances
                .GroupBy(a => a.AttendanceDate.Date)
                .Select(g => new AttendanceChartDto
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),

                    PresentCount =
                        g.Count(a => a.Status == "Present")
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }
    
    public async Task<LeaveStatisticsDto>
    GetLeaveStatisticsAsync()
        {
            return new LeaveStatisticsDto
            {
                PendingLeaves =
                    await _context.LeaveRequests
                        .CountAsync(l => l.Status == "Pending"),

                ApprovedLeaves =
                    await _context.LeaveRequests
                        .CountAsync(l => l.Status == "Approved"),

                RejectedLeaves =
                    await _context.LeaveRequests
                        .CountAsync(l => l.Status == "Rejected")
            };
        }
    public async Task<ProjectStatisticsDto>
    GetProjectStatisticsAsync()
        {
            return new ProjectStatisticsDto
            {
                ActiveProjects =
                    await _context.Projects
                        .CountAsync(p => p.Status == "Active"),

                CompletedProjects =
                    await _context.Projects
                        .CountAsync(p => p.Status == "Completed")
            };
        }

    }
}