using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}