using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class CheckOutDto
    {
        [Required]
        public int EmployeeId { get; set; }
    }
}