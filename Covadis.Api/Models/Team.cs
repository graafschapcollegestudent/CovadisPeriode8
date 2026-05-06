namespace Covadis.Api.Models;

public class Team
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; }
    
    public List<User> Users { get; set; } = new();
    public List<Task> Tasks { get; set; } = new();
}