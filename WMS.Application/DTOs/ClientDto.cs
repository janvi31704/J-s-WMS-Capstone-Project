namespace WMS.Application.DTOs
{
    public class ClientDto
    {
        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public string? ClientAddress { get; set; }

        public decimal? ClientPhoneNumber { get; set; }

        public string? ClientLocation { get; set; }

        public bool Status { get; set; }
    }
}