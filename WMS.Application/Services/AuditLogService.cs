using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;

namespace WMS.Application.Services
{
    public class AuditLogService
        : IAuditLogService
    {
        private readonly
            IAuditLogRepository _repository;

        public AuditLogService(
            IAuditLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AuditLogDto>>
            GetAllAsync()
        {
            var logs = await _repository.GetAllAsync();

            return logs.Select(a => new AuditLogDto
            {
                AuditId = a.AuditId,
                EntityName = a.EntityName,
                RecordId = a.RecordId,
                Action = a.Action,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn
            });
        }

        public async Task AddAsync(
            string entityName,
            int recordId,
            string action,
            int createdBy)
        {
            var audit = new AuditLog
            {
                EntityName = entityName,
                RecordId = recordId,
                Action = action,
                CreatedBy = createdBy,
                CreatedOn = DateTime.Now
            };

            await _repository.AddAsync(audit);
        }
    }
}