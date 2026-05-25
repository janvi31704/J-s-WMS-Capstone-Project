using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;  

        [StringLength(255)]
        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public ICollection<Employee>? Employees { get; set; }
    }
}