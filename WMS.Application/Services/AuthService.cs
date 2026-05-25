using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;

namespace WMS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IAuthRepository repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task RegisterAsync(RegisterUserDto dto)
        {
            var existingUser =
                await _repository.GetUserByUsernameAsync(dto.Username);

            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            var user = new UserLogin
            {
                Username = dto.Username,
                Password = dto.Password,
                EmployeeId = dto.EmployeeId,
                RoleId = dto.RoleId
            };

            await _repository.AddUserAsync(user);
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            var user =
                await _repository.GetUserByUsernameAsync(dto.Username);

            if (user == null || user.Password != dto.Password)
            {
                throw new Exception("Invalid username or password.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),

                new Claim(ClaimTypes.Role,
                    user.Role?.RoleName ?? "Employee")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds);

            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(token),

                Username = user.Username,

                Role = user.Role?.RoleName ?? "Employee"
            };
        }
    }
}