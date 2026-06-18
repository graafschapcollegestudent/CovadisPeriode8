using Covadis.Api.Application.DTOs.Team;
using Covadis.Api.Application.Interfaces;
using Covadis.Api.Application.Services;
using Covadis.Api.Generics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Covadis.Api.Application.Interfaces;
using System.Security.Claims;
using Covadis.Api.Application.DTOs.Task;

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

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<TeamReadDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<ActionResult> GetAllTeamsAsync()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            
            if (teams == null)
                return NotFound(ApiResponse<string>.Fail("Geen teams gevonden."));
            
            return Ok(ApiResponse<List<TeamReadDto>>.Ok(teams, "Teams succesvol opgehaald."));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TeamReadDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<ActionResult> GetTeamByIdAsync(Guid id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            
            if (team == null)
                return NotFound(ApiResponse<string>.Fail("Team niet gevonden."));
            
            return Ok(ApiResponse<TeamReadDto>.Ok(team, "Team succesvol opgehaald."));
        }

        [HttpGet("{id}/tasks")]
        [ProducesResponseType(typeof(ApiResponse<List<TaskListDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<ActionResult> GetTasksFromTeamAsync(Guid id)
        {
            var tasks = await _teamService.GetTasksFromTeamAsync(id);

            if (tasks == null)
                return NotFound(ApiResponse<string>.Fail("Geen taken gevonden voor dit team."));

            return Ok(ApiResponse<List<TaskListDto>>.Ok(tasks, "Taken succesvol opgehaald."));
        }
    }
}
