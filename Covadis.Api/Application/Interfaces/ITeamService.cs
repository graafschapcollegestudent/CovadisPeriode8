using Covadis.Api.Application.DTOs.Team;

namespace Covadis.Api.Application.Interfaces;

public interface ITeamService
{
    Task<List<TeamReadDto>?> GetAllTeamsAsync();
    Task<TeamReadDto?> GetTeamByIdAsync(Guid id);
}