using Covadis.Api.Application.DTOs.Task;
using Covadis.Api.Application.DTOs.Team;

namespace Covadis.Api.Application.Interfaces;
using Covadis.Api.Application.DTOs.Team;

public interface ITeamService
{
    Task<List<TeamReadDto>?> GetAllTeamsAsync();
    Task<TeamReadDto?> GetTeamByIdAsync(Guid id);
    Task<List<TaskListDto>?> GetTasksFromTeamAsync(Guid id);
}