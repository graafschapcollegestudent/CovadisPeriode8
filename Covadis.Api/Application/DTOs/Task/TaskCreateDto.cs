using Covadis.Api.Models;

namespace Covadis.Api.Application.DTOs.Task;

public class TaskCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Status Status { get; set; }
    public Guid TeamId { get; set; }
}