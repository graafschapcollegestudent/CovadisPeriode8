namespace Covadis.Api.Models;

public enum UserRole
{
    Planner,
    Manager
}

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
}