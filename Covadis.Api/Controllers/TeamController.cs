using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Covadis.Api.Application.Interfaces;
using System.Security.Claims;

namespace Covadis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpGet("my-team")]
        public async Task<IActionResult> GetMyTeam()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(userIdString);
            var team = await _teamService.GetTeamByUserIdAsync(userId);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }
    }
}
