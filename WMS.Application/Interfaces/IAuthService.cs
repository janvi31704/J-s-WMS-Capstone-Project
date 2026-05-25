using WMS.Application.DTOs;

namespace WMS.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterUserDto dto);

        Task<LoginResponseDto?> LoginAsync(LoginDto dto);
    }
}