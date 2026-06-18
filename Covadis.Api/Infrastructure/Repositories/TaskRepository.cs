using Covadis.Api.Application.Interfaces;
using Covadis.Api.Data;
using Microsoft.EntityFrameworkCore;
using TaskItem = Covadis.Api.Models.Task;


namespace Covadis.Api.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
    
    public TaskRepository(AppDbContext dbContext)
    {
        _context = dbContext;
    }
    
    public async Task<TaskItem?> GetTaskByIdAsync(Guid taskId)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
    }
    
    public async Task UpdateTaskAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }
}