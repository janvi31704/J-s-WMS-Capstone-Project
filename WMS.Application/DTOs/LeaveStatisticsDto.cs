namespace WMS.Application.DTOs
{
    public class LeaveStatisticsDto
    {
        public int PendingLeaves { get; set; }

        public int ApprovedLeaves { get; set; }

        public int RejectedLeaves { get; set; }
    }
}