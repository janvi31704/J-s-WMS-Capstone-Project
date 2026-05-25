using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class UpdateAnnouncementDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsActive { get; set; }
    }
}