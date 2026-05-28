using Microsoft.EntityFrameworkCore;
using WMS.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WMS.Application.Interfaces;
using WMS.Application.Services;
using WMS.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using WMS.Infrastructure.Services;
using WMS.API.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace WMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.Configure<ApiBehaviorOptions>(
                options =>
                {
                    options.InvalidModelStateResponseFactory =
                        context =>
                        {
                            return new BadRequestObjectResult(
                                new
                                {
                                    message =
                                        "Validation failed",

                                    errors =
                                        context.ModelState
                                });
                        };
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter JWT Token"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});

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

            builder.Services.AddScoped<IClientRepository, ClientRepository>();

            builder.Services.AddScoped<IClientService, ClientService>();

            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

            builder.Services.AddScoped<IProjectService, ProjectService>();

            builder.Services.AddScoped<IEmployeeProjectAllocationRepository,EmployeeProjectAllocationRepository>();

            builder.Services.AddScoped<IEmployeeProjectAllocationService,EmployeeProjectAllocationService>();

            builder.Services.AddScoped<IDashboardService,DashboardService>();

            builder.Services.AddScoped<IAnnouncementRepository,AnnouncementRepository>();

            builder.Services.AddScoped<IAnnouncementService,AnnouncementService>();

            builder.Services.AddScoped<IAuditLogRepository,AuditLogRepository>();

            builder.Services.AddScoped<IAuditLogService,AuditLogService>();
            
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

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            
            
                app.UseSwagger();
                app.UseSwaggerUI();
            

            

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}