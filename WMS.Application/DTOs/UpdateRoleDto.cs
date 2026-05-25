using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class UpdateRoleDto
    {
        [Required]
        public string RoleName { get; set; }

        public string? Description { get; set; }
    }
}