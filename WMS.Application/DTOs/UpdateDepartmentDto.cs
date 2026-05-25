using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class UpdateDepartmentDto
    {
        [Required]
        public string DepartmentName { get; set; }

        public string? Description { get; set; }
    }
}