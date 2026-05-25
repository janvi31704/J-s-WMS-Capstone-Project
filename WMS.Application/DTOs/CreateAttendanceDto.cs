using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class CreateAttendanceDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; }

        public string? Remarks { get; set; }
    }
}