using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;

namespace WMS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<EmployeeProjectAllocation> EmployeeProjectAllocations { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RoleId);

                modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId);

                modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.Employee)
                .WithMany()
                .HasForeignKey(l => l.EmployeeId);

               modelBuilder.Entity<UserLogin>()
                .HasOne(u => u.Employee)
                .WithMany()
                .HasForeignKey(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<UserLogin>()
                    .HasOne(u => u.Role)
                    .WithMany()
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.NoAction);
                modelBuilder.Entity<Project>()
                    .HasOne(p => p.Client)
                    .WithMany()
                    .HasForeignKey(p => p.ClientId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<EmployeeProjectAllocation>()
                    .HasOne(e => e.Employee)
                    .WithMany()
                    .HasForeignKey(e => e.EmpId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<EmployeeProjectAllocation>()
                    .HasOne(e => e.Project)
                    .WithMany()
                    .HasForeignKey(e => e.ProjectId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}