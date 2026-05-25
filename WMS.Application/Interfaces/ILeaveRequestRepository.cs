using WMS.Domain.Entities;

namespace WMS.Application.Interfaces
{
    public interface ILeaveRequestRepository
    {
        Task<IEnumerable<LeaveRequest>> GetAllAsync();

        Task<LeaveRequest?> GetByIdAsync(int id);

        Task AddAsync(LeaveRequest leaveRequest);

        Task UpdateAsync(LeaveRequest leaveRequest);
    }
}