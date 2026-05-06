using Covadis.Api.Application.DTOs.Team;
using Covadis.Api.Application.Extensions;
using Covadis.Api.Application.Interfaces;
using Covadis.Api.Models;

namespace Covadis.Api.Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    
    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task<List<TeamReadDto>?> GetAllTeamsAsync()
    {
        var teams = await _teamRepository.GetAllAsync();
        return teams?.Select(t => t.ToReadDto()).ToList();
    }
}