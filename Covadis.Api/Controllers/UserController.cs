using System.Security.Claims;
using Covadis.Api.Application.DTOs.Team;
using Covadis.Api.Application.Interfaces;
using Covadis.Api.Generics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Covadis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("team")]
        [ProducesResponseType(typeof(ApiResponse<TeamReadDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<ActionResult> GetTeamFromUser()
        {
            // Try common JWT claim names for the user id
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var team = await _userService.GetTeamFromUser(Guid.Parse(userId));
            if (team == null)
                return NotFound(ApiResponse<string>.Fail("Team niet gevonden voor deze gebruiker."));

            return Ok(ApiResponse<TeamReadDto>.Ok(team, "Team succesvol opgehaald voor deze gebruiker."));
        }
    }
}
