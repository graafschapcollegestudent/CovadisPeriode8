using Covadis.Api.Models;
using Task = Covadis.Api.Models.Task;

namespace Covadis.Api.Application.DTOs.Team;

public class TeamListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public List<Models.User> Users { get; set; }
    public List<Models.Task> Tasks { get; set; }
}