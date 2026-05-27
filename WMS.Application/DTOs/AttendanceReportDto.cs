namespace WMS.Application.DTOs
{
    public class AttendanceReportDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DateTime AttendanceDate { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public decimal? WorkingHours { get; set; }

        public string Status { get; set; }
    }
}