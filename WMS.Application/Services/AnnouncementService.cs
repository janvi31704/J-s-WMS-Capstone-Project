using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;

namespace WMS.Application.Services
{
    public class AnnouncementService
        : IAnnouncementService
    {
        private readonly
            IAnnouncementRepository _repository;

        public AnnouncementService(
            IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AnnouncementDto>>
            GetAllAsync()
        {
            var announcements =
                await _repository.GetAllAsync();

            return announcements.Select(a =>
                new AnnouncementDto
                {
                    AnnouncementId =
                        a.AnnouncementId,

                    Title = a.Title,

                    Message = a.Message,

                    CreatedBy = a.CreatedBy,

                    CreatedOn = a.CreatedOn,

                    IsActive = a.IsActive
                });
        }

        public async Task<AnnouncementDto?>
            GetByIdAsync(int id)
        {
            var announcement =
                await _repository.GetByIdAsync(id);

            if (announcement == null)
                return null;

            return new AnnouncementDto
            {
                AnnouncementId =
                    announcement.AnnouncementId,

                Title = announcement.Title,

                Message = announcement.Message,

                CreatedBy = announcement.CreatedBy,

                CreatedOn = announcement.CreatedOn,

                IsActive = announcement.IsActive
            };
        }

        public async Task AddAsync(
            CreateAnnouncementDto dto)
        {
            var announcement =
                new Announcement
                {
                    Title = dto.Title,

                    Message = dto.Message,

                    CreatedBy = dto.CreatedBy,

                    CreatedOn = DateTime.Now,

                    IsActive = true
                };

            await _repository.AddAsync(
                announcement);
        }

        public async Task UpdateAsync(
            int id,
            UpdateAnnouncementDto dto)
        {
            var announcement =
                await _repository.GetByIdAsync(id);

            if (announcement == null)
                return;

            announcement.Title = dto.Title;

            announcement.Message = dto.Message;

            announcement.IsActive = dto.IsActive;

            await _repository.UpdateAsync(
                announcement);
        }

        public async Task DeleteAsync(int id)
        {
            var announcement =
                await _repository.GetByIdAsync(id);

            if (announcement == null)
                return;

            await _repository.DeleteAsync(
                announcement);
        }
    }
}