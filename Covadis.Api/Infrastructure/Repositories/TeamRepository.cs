using Covadis.Api.Application.Interfaces;
using Covadis.Api.Data;
using Covadis.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Covadis.Api.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _context;

    public TeamRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Team>?> GetAllAsync()
    {
        return await _context.Teams
            .Include(t => t.Tasks)
            .Include(t => t.Users)
            .ToListAsync();
    }

    public async Task<Team?> GetByIdAsync(Guid id)
    {
        return await _context.Teams
            .Include(t => t.Tasks)
            .Include(t => t.Users)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Team?> GetByUserIdAsync(Guid userId)
    {
        var user = await _context.Users
            .Include(u => u.Team)
            .FirstOrDefaultAsync(u => u.Id == userId);
        return user?.Team;
    }
}