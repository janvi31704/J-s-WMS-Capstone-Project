using WMS.Application.DTOs;

namespace WMS.Application.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnnouncementDto>> GetAllAsync();

        Task<AnnouncementDto?> GetByIdAsync(int id);

        Task AddAsync(CreateAnnouncementDto dto);

        Task UpdateAsync(int id, UpdateAnnouncementDto dto);

        Task DeleteAsync(int id);
    }
}