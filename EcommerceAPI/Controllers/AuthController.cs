// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using EcommerceAPI.DTOs;
using EcommerceAPI.Services;
using EcommerceAPI.Helpers;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EcommerceAPI.Services.Interfaces.IAuthService _authService;

        public AuthController(EcommerceAPI.Services.Interfaces.IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<string>>> Register(UserRegisterDto request)
        {
            var response = await _authService.Register(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authService.Login(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}