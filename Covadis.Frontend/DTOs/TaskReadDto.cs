namespace Covadis.Frontend.DTOs;

public class TaskReadDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime EstimatedDuration { get; set; }
}