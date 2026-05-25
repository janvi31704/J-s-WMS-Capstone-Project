using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class LeaveApprovalDto
    {
        [Required]
        public int ApprovedBy { get; set; }

        public string? Remarks { get; set; }
    }
}