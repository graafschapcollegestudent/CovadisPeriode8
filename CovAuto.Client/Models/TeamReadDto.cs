namespace Covadis.Client.Models;

public class TeamReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<UserReadDto> Users { get; set; } = new();
    public List<TaskReadDto> Tasks { get; set; } = new();
}