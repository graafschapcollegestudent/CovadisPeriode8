namespace Covadis.Frontend.DTOs;

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; } = DateTime.Now;
    public int Status { get; set; }
    public int SprintNumber { get; set; }
    public Guid TeamId { get; set; }
}