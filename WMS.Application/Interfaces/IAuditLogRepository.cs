using WMS.Domain.Entities;

namespace WMS.Application.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLog>> GetAllAsync();

        Task AddAsync(AuditLog auditLog);
    }
}