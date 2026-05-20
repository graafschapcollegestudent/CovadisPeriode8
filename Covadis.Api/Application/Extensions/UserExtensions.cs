using Covadis.Api.Application.DTOs.User;
using Covadis.Api.Models;

namespace Covadis.Api.Application.Extensions;

public static class UserExtensions
{
    public static UserReadDto ToReadDto(this User user)
    {
        return new UserReadDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            FullName = user.FullName,
            Role = user.Role,
            TeamId = user.TeamId,
        };
    }
    
    public static UserListDto ToListDto(this User user)
    {
        return new UserListDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
            TeamId = user.TeamId,
        };
    }
}