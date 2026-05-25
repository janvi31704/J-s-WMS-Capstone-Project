namespace WMS.Application.DTOs
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public string Status { get; set; }

        public decimal? WorkingHours { get; set; }

        public string? Remarks { get; set; }
    }
}