using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;

namespace WMS.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repository;

        public AttendanceService(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAsync()
        {
            var attendanceList = await _repository.GetAllAsync();

            return attendanceList.Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EmployeeId = a.EmployeeId,
                AttendanceDate = a.AttendanceDate,
                CheckInTime = a.CheckInTime,
                CheckOutTime = a.CheckOutTime,
                Status = a.Status,
                WorkingHours = a.WorkingHours,
                Remarks = a.Remarks
            });
        }

        public async Task<AttendanceDto?> GetByIdAsync(int id)
        {
            var attendance = await _repository.GetByIdAsync(id);

            if (attendance == null)
                return null;

            return new AttendanceDto
            {
                AttendanceId = attendance.AttendanceId,
                EmployeeId = attendance.EmployeeId,
                AttendanceDate = attendance.AttendanceDate,
                CheckInTime = attendance.CheckInTime,
                CheckOutTime = attendance.CheckOutTime,
                Status = attendance.Status,
                WorkingHours = attendance.WorkingHours,
                Remarks = attendance.Remarks
            };
        }

        public async Task CheckInAsync(CheckInDto dto)
        {
            var existingAttendance =
                await _repository.GetTodayAttendanceAsync(
                    dto.EmployeeId,
                    DateTime.Today);

            if (existingAttendance != null)
            {
                throw new Exception("Employee already checked in today.");
            }

            var attendance = new Attendance
            {
                EmployeeId = dto.EmployeeId,
                AttendanceDate = DateTime.Today,
                CheckInTime = DateTime.Now,
                Status = "Present"
            };

            // Late marking logic
            if (DateTime.Now.TimeOfDay > new TimeSpan(9, 30, 0))
            {
                attendance.Status = "Late";
            }

            await _repository.AddAsync(attendance);

        
        }

        public async Task CheckOutAsync(CheckOutDto dto)
        {
            var attendance =
                await _repository.GetTodayAttendanceAsync(
                    dto.EmployeeId,
                    DateTime.Today);

            if (attendance == null)
            {
                throw new Exception("Check-in record not found.");
            }

            if (attendance.CheckOutTime != null)
            {
                throw new Exception("Employee already checked out.");
            }

            attendance.CheckOutTime = DateTime.Now;

            // Calculate Working Hours
            if (attendance.CheckInTime != null)
            {
                var totalHours =
                    attendance.CheckOutTime.Value -
                    attendance.CheckInTime.Value;

                attendance.WorkingHours =
                    (decimal)totalHours.TotalHours;

                // Half-day logic
                if (attendance.WorkingHours < 4)
                {
                    attendance.Status = "Half-Day";
                }
            }

            await _repository.UpdateAsync(attendance);
        }

        public async Task<IEnumerable<AttendanceDto>> GetMonthlyAttendanceAsync(
            int employeeId,
            int month,
            int year)
        {
            var attendance =
                await _repository.GetMonthlyAttendanceAsync(
                    employeeId,
                    month,
                    year);

            return attendance.Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EmployeeId = a.EmployeeId,
                AttendanceDate = a.AttendanceDate,
                CheckInTime = a.CheckInTime,
                CheckOutTime = a.CheckOutTime,
                Status = a.Status,
                WorkingHours = a.WorkingHours,
                Remarks = a.Remarks
            });
}

public async Task<decimal> GetTotalWorkingHoursAsync(
    int employeeId,
    int month,
    int year)
{
    var attendance =
        await _repository.GetMonthlyAttendanceAsync(
            employeeId,
            month,
            year);

    return attendance.Sum(a => a.WorkingHours ?? 0);
}

public async Task<IEnumerable<AttendanceReportDto>>
    GetAttendanceReportAsync()
{
    var attendance =
        await _repository.GetAllAsync();

    return attendance.Select(a =>
        new AttendanceReportDto
        {
            EmployeeId = a.EmployeeId,

            EmployeeName =
                a.Employee != null
                ? a.Employee.FirstName + " " +
                  a.Employee.LastName
                : "N/A",

            AttendanceDate = a.AttendanceDate,

            CheckInTime = a.CheckInTime,

            CheckOutTime = a.CheckOutTime,

            WorkingHours = a.WorkingHours,

            Status = a.Status
        });
}
    }

}