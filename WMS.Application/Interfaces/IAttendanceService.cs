using WMS.Application.DTOs;

namespace WMS.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDto>> GetAllAsync();

        Task<AttendanceDto?> GetByIdAsync(int id);

        Task CheckInAsync(CheckInDto dto);

        Task CheckOutAsync(CheckOutDto dto);

        Task<IEnumerable<AttendanceReportDto>>GetAttendanceReportAsync();

        Task<IEnumerable<AttendanceDto>> GetMonthlyAttendanceAsync(
            int employeeId,
            int month,
            int year);

        Task<decimal> GetTotalWorkingHoursAsync(
            int employeeId,
            int month,
            int year);
            }
}