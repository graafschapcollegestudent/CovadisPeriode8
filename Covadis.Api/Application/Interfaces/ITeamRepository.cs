using Covadis.Api.Models;

namespace Covadis.Api.Application.Interfaces;

public interface ITeamRepository
{
    Task<List<Team>?> GetAllAsync();
    Task<Team?> GetByIdAsync(Guid id);
}