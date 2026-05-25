using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class UpdateEmployeeDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public DateTime DOJ { get; set; }

        public int DepartmentId { get; set; }

        public int RoleId { get; set; }
    }
}