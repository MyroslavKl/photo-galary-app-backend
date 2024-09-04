using AlbumProject.DTOs;

using AlbumProject.Persistence.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AlbumProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDto loginDto)
        {
            await _authenticationService.RegisterAsync(loginDto.Username, loginDto.Password);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _authenticationService.Authenticate(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            var token = _authenticationService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }
    
}
