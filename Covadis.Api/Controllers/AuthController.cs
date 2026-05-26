using Covadis.Api.Application.DTOs;
using Covadis.Api.Application.Interfaces;
using Covadis.Api.Generics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Covadis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 401)]
        public async Task<ActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
                return Unauthorized(ApiResponse<string>.Fail("Ongeldige email of wachtwoord."));

            return Ok(ApiResponse<LoginResponse>.Ok(result, "Succesvol ingelogd"));
        }
    }
}
