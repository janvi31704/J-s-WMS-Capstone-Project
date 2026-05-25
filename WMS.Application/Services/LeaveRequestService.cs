using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;

namespace WMS.Application.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _repository;

        public LeaveRequestService(ILeaveRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetAllAsync()
        {
            var leaves = await _repository.GetAllAsync();

            return leaves.Select(l => new LeaveRequestDto
            {
                LeaveRequestId = l.LeaveRequestId,
                EmployeeId = l.EmployeeId,
                LeaveType = l.LeaveType,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Reason = l.Reason,
                Status = l.Status,
                AppliedOn = l.AppliedOn,
                ApprovedBy = l.ApprovedBy,
                Remarks = l.Remarks
            });
        }

        public async Task<LeaveRequestDto?> GetByIdAsync(int id)
        {
            var leave = await _repository.GetByIdAsync(id);

            if (leave == null)
                return null;

            return new LeaveRequestDto
            {
                LeaveRequestId = leave.LeaveRequestId,
                EmployeeId = leave.EmployeeId,
                LeaveType = leave.LeaveType,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Reason = leave.Reason,
                Status = leave.Status,
                AppliedOn = leave.AppliedOn,
                ApprovedBy = leave.ApprovedBy,
                Remarks = leave.Remarks
            };
        }

        public async Task ApplyLeaveAsync(CreateLeaveRequestDto dto)
        {
            if (dto.EndDate < dto.StartDate)
            {
                throw new Exception("End date cannot be before start date.");
            }

            var leave = new LeaveRequest
            {
                EmployeeId = dto.EmployeeId,
                LeaveType = dto.LeaveType,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Reason = dto.Reason,
                Status = "Pending"
            };

            await _repository.AddAsync(leave);
        }

        public async Task ApproveLeaveAsync(int id, LeaveApprovalDto dto)
        {
            var leave = await _repository.GetByIdAsync(id);

            if (leave == null)
                throw new Exception("Leave request not found.");

            leave.Status = "Approved";
            leave.ApprovedBy = dto.ApprovedBy;
            leave.Remarks = dto.Remarks;

            await _repository.UpdateAsync(leave);
        }

        public async Task RejectLeaveAsync(int id, LeaveApprovalDto dto)
        {
            var leave = await _repository.GetByIdAsync(id);

            if (leave == null)
                throw new Exception("Leave request not found.");

            leave.Status = "Rejected";
            leave.ApprovedBy = dto.ApprovedBy;
            leave.Remarks = dto.Remarks;

            await _repository.UpdateAsync(leave);
        }
    }
}