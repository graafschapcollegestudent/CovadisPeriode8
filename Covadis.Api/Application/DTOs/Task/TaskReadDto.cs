using Covadis.Api.Models;

namespace Covadis.Api.Application.DTOs.Task;

public class TaskReadDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public DateTime DueDate { get; set; }
    public int EstimatedDuration { get; set; }
    public Guid? TeamId { get; set; }
    public Models.Team? Team { get; set; }
}