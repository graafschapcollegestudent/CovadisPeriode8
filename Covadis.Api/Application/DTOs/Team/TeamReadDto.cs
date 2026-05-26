using Covadis.Api.Application.DTOs.Task;
using Covadis.Api.Application.DTOs.User;

namespace Covadis.Api.Application.DTOs.Team;

public class TeamReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public List<UserReadDto> Users { get; set; }
    public List<TaskReadDto> Tasks { get; set; }
}