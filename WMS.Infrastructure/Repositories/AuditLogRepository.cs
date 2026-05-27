using Microsoft.EntityFrameworkCore;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class AuditLogRepository
        : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuditLog>>
            GetAllAsync()
        {
            return await _context.AuditLogs
                .OrderByDescending(a => a.CreatedOn)
                .ToListAsync();
        }

        public async Task AddAsync(AuditLog auditLog)
        {
            await _context.AuditLogs
                .AddAsync(auditLog);

            await _context.SaveChangesAsync();
        }
    }
}