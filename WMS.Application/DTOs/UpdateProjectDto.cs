using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class UpdateProjectDto
    {
        [Required]
        public string ProjectName { get; set; }

        public int? ClientId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Status { get; set; }
    }
}