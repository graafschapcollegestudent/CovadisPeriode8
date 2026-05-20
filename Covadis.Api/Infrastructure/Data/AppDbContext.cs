using Covadis.Api.Models;
using Microsoft.EntityFrameworkCore;
using TaskItem = Covadis.Api.Models.Task;

namespace Covadis.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Team>(team =>
        {
            // Team (1) -> Users (N)
            team.HasMany(t => t.Users)
                .WithOne(u => u.Team)
                .HasForeignKey(u => u.TeamId);

            // Team (1) -> Tasks (N)
            team.HasMany(t => t.Tasks)
                .WithOne(task => task.Team)
                .HasForeignKey(task => task.TeamId);
        });
    } 
}