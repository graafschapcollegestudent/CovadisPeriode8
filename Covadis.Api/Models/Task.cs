namespace Covadis.Api.Models;

public enum Status
{
    NotStarted,
    InProgress,
    Completed
}

public class Task
{
    public Guid Id { get; set; } = new Guid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; }
    public DateTime DueDate { get; set; }
    public int EstimatedDuration { get; set; }

    public int SprintNumber { get; set; }
    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
}