using Covadis.Api.Application.DTOs.Task;
using Covadis.Api.Application.DTOs.User;
using Covadis.Api.Models;
using Task = Covadis.Api.Models.Task;

namespace Covadis.Api.Application.DTOs.Team;

public class TeamReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public List<UserListDto> Users { get; set; }
    public List<TaskListDto> Tasks { get; set; }
}