using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class CreateEmployeeProjectAllocationDto
    {
        [Required]
        public int EmpId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public DateTime AssignedOn { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}