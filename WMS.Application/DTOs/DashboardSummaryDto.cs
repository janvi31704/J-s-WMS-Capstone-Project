namespace WMS.Application.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalEmployees { get; set; }

        public int TotalDepartments { get; set; }

        public int TotalProjects { get; set; }

        public int PendingLeaves { get; set; }

        public int PresentEmployeesToday { get; set; }
    }
}