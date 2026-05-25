using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterUserDto dto)
        {
            try
            {
                await _service.RegisterAsync(dto);

                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginDto dto)
        {
            try
            {
                var result = await _service.LoginAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}