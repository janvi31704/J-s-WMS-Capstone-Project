using WMS.Application.DTOs;

namespace WMS.Application.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequestDto>> GetAllAsync();

        Task<LeaveRequestDto?> GetByIdAsync(int id);

        Task ApplyLeaveAsync(CreateLeaveRequestDto dto);

        Task ApproveLeaveAsync(int id, LeaveApprovalDto dto);

        Task RejectLeaveAsync(int id, LeaveApprovalDto dto);
    }
}