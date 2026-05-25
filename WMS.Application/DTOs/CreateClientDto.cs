using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs
{
    public class CreateClientDto
    {
        [Required]
        public string ClientName { get; set; }

        public string? ClientAddress { get; set; }

        public decimal? ClientPhoneNumber { get; set; }

        public string? ClientLocation { get; set; }

        public bool Status { get; set; } = true;
    }
}