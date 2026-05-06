namespace Covadis.Api.Models;

public enum UserRole
{
    Developer,
    Manager
}

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Developer;

    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
}