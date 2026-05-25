using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        public int? ClientId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Active";

        // Navigation Property
        public Client? Client { get; set; }
    }
}