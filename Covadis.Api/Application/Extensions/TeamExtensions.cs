using Covadis.Api.Application.DTOs.Team;
using Covadis.Api.Models;

namespace Covadis.Api.Application.Extensions;

public static class TeamExtensions
{
    public static TeamReadDto ToReadDto(this Team team)
    {
        return new TeamReadDto
        {
            Id = team.Id,
            Name = team.Name,
            Users = team.Users.Select(u => u.ToReadDto()).ToList(),
            Tasks = team.Tasks.Select(t => t.ToReadDto()).ToList()
        };
    }

    public static TeamListDto ToListDto(this Team team)
    {
        return new TeamListDto
        {
            Id = team.Id,
            Name = team.Name,
            Users = team.Users,
            Tasks = team.Tasks
        };
    } 
}