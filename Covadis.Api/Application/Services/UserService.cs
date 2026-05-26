using Covadis.Api.Application.DTOs.Team;
using Covadis.Api.Application.DTOs.User;
using Covadis.Api.Application.Extensions;
using Covadis.Api.Application.Interfaces;
using Covadis.Api.Models;

namespace Covadis.Api.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITeamRepository _teamRepository;
    
    public UserService(IUserRepository userRepository, ITeamRepository teamRepository)
    {
        _userRepository = userRepository;
        _teamRepository = teamRepository;
    }

    public async Task<TeamReadDto?> GetTeamFromUser(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        
        if (user == null)
            return null;
        
        if (user.Team != null)
            return user.Team.ToReadDto();

        if (!user.TeamId.HasValue)
            return null;

        var team = await _teamRepository.GetByIdAsync(user.TeamId.Value);
        return team?.ToReadDto();
    }
}