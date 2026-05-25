using WMS.Domain.Entities;

namespace WMS.Application.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAsync();

        Task<Attendance?> GetByIdAsync(int id);

        Task AddAsync(Attendance attendance);

        Task UpdateAsync(Attendance attendance);

        Task<Attendance?> GetTodayAttendanceAsync(int employeeId, DateTime date);

        Task<IEnumerable<Attendance>> GetMonthlyAttendanceAsync(
            int employeeId,
            int month,
            int year);
    }
}