using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class CreateDepartmentDto
    {
        [Required]
        public string DepartmentName { get; set; }

        public string? Description { get; set; }
    }
}