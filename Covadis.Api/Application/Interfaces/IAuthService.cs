using Covadis.Api.Application.DTOs;

namespace Covadis.Api.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}