using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class UpdateEmployeeProjectAllocationDto
    {
        [Required]
        public bool Status { get; set; }

        public string? UpdatedBy { get; set; }
    }
}