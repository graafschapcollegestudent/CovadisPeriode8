using Covadis.Api.Models;

namespace Covadis.Api.Application.DTOs.User;

public class UserReadDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public UserRole Role { get; set; }

    public Guid? TeamId { get; set; }
}