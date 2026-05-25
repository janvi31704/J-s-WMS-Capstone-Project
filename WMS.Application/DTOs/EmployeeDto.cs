namespace WMS.Application.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public char Gender { get; set; }

        public DateTime DOB { get; set; }

        public DateTime DOJ { get; set; }

        public int DepartmentId { get; set; }

        public int RoleId { get; set; }

        public string Status { get; set; }
    }
}