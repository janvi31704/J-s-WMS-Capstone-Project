using WMS.Application.DTOs;

namespace WMS.Application.Interfaces
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogDto>> GetAllAsync();

        Task AddAsync(
            string entityName,
            int recordId,
            string action,
            int createdBy);
    }
}