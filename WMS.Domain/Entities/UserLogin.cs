using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities
{
    public class UserLogin
    {
        [Key]
        public int UserLoginId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        // Navigation Properties
        public Employee? Employee { get; set; }

        public Role? Role { get; set; }
    }
}