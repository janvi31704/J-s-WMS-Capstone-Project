using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class CreateLeaveRequestDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? Reason { get; set; }
    }
}