using Covadis.Api.Application.DTOs.Team;

namespace Covadis.Api.Application.Interfaces;

public interface IUserService
{
    Task<TeamReadDto?> GetTeamFromUser(Guid userId);
}