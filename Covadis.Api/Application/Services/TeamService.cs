using Covadis.Api.Application.DTOs.Task;
using Covadis.Api.Application.DTOs.Team;
using Covadis.Api.Application.Extensions;
using Covadis.Api.Application.Interfaces;
using Covadis.Api.Models;
using Microsoft.AspNetCore.Authorization;

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

    public async Task<TeamReadDto?> GetTeamByIdAsync(Guid id)
    {
        var team = await _teamRepository.GetByIdAsync(id);

        if (team == null)
            return null;
        
        return team.ToReadDto();
    }

    public async Task<List<TaskListDto>?> GetTasksFromTeamAsync(Guid id)
    {
        var team = await _teamRepository.GetByIdAsync(id);

        if (team == null)
            return null;
        
        return team.Tasks.Select(t => t.ToListDto()).ToList();
    }
}