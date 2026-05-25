using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string LeaveType { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(255)]
        public string? Reason { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        public DateTime AppliedOn { get; set; } = DateTime.Now;

        public int? ApprovedBy { get; set; }

        [StringLength(255)]
        public string? Remarks { get; set; }

        // Navigation Property
        public Employee? Employee { get; set; }
    }
}