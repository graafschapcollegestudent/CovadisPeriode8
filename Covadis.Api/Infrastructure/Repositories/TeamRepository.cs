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
        return await _context.Teams.ToListAsync();
    }

    public async Task<Team?> GetByIdAsync(Guid id)
    {
        return await _context.Teams.FindAsync(id);
    }
}