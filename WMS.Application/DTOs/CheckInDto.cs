using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class CheckInDto
    {
        [Required]
        public int EmployeeId { get; set; }

        
    }
}