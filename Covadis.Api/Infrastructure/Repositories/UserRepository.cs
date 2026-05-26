using Covadis.Api.Application.Interfaces;
using Covadis.Api.Data;
using Covadis.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Covadis.Api.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

   public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Team)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}