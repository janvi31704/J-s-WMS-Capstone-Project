using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;

namespace WMS.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var clients = await _repository.GetAllAsync();

            return clients.Select(c => new ClientDto
            {
                ClientId = c.ClientId,
                ClientName = c.ClientName,
                ClientAddress = c.ClientAddress,
                ClientPhoneNumber = c.ClientPhoneNumber,
                ClientLocation = c.ClientLocation,
                Status = c.Status
            });
        }

        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);

            if (client == null)
                return null;

            return new ClientDto
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                ClientAddress = client.ClientAddress,
                ClientPhoneNumber = client.ClientPhoneNumber,
                ClientLocation = client.ClientLocation,
                Status = client.Status
            };
        }

        public async Task AddAsync(CreateClientDto dto)
        {
            var client = new Client
            {
                ClientName = dto.ClientName,
                ClientAddress = dto.ClientAddress,
                ClientPhoneNumber = dto.ClientPhoneNumber,
                ClientLocation = dto.ClientLocation,
                Status = dto.Status
            };

            await _repository.AddAsync(client);
        }

        public async Task UpdateAsync(int id, UpdateClientDto dto)
        {
            var client = await _repository.GetByIdAsync(id);

            if (client == null)
                return;

            client.ClientName = dto.ClientName;
            client.ClientAddress = dto.ClientAddress;
            client.ClientPhoneNumber = dto.ClientPhoneNumber;
            client.ClientLocation = dto.ClientLocation;
            client.Status = dto.Status;

            await _repository.UpdateAsync(client);
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);

            if (client == null)
                return;

            await _repository.DeleteAsync(client);
        }
    }
}