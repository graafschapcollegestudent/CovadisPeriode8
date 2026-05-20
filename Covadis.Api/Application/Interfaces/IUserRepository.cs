using Covadis.Api.Models;

namespace Covadis.Api.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string username);
}