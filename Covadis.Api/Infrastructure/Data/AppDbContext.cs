using Covadis.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Covadis.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
}