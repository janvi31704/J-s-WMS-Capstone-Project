using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;

namespace WMS.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await _repository.GetAllAsync();

            return roles.Select(r => new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
                Description = r.Description
            });
        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id);

            if (role == null)
                return null;

            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description
            };
        }

        public async Task AddAsync(CreateRoleDto dto)
        {
            var role = new Role
            {
                RoleName = dto.RoleName,
                Description = dto.Description
            };

            await _repository.AddAsync(role);
        }

        public async Task UpdateAsync(int id, UpdateRoleDto dto)
        {
            var role = await _repository.GetByIdAsync(id);

            if (role == null)
                return;

            role.RoleName = dto.RoleName;
            role.Description = dto.Description;

            await _repository.UpdateAsync(role);
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id);

            if (role == null)
                return;

            await _repository.DeleteAsync(role);
        }
    }
}