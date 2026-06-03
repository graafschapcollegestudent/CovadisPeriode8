namespace Covadis.Frontend.DTOs;

public class UserReadDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid? TeamId { get; set; }
}