using ECom.Application.DTOs.IdentityDTO;
using ECom.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService _authenticationService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUser model)
        {
            var result = await _authenticationService.CreateUser(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser model)
        {
            var result = await _authenticationService.LoginUser(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("refreshToken/{refreshToken}")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _authenticationService.RefreshToken(refreshToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
