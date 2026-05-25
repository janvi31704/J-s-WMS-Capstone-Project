using Microsoft.EntityFrameworkCore;
using WMS.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WMS.Application.Interfaces;
using WMS.Application.Services;
using WMS.Infrastructure.Repositories;

namespace WMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Dependency Injection
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            builder.Services.AddScoped<IRoleService, RoleService>();

            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

            builder.Services.AddScoped<IAttendanceService, AttendanceService>();

            builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

            builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();

            builder.Services.AddScoped<IAuthRepository, AuthRepository>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            builder.Services.AddAuthorization();
            

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            app.UseCors("AllowAngular");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}