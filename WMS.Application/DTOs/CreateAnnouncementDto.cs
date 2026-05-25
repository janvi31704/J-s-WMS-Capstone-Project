using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class CreateAnnouncementDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public int CreatedBy { get; set; }
    }
}