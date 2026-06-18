using TaskItem = Covadis.Api.Models.Task;

namespace Covadis.Api.Application.Interfaces;

public interface ITaskRepository
{
    Task<TaskItem?> GetTaskByIdAsync(Guid taskId);
    Task UpdateTaskAsync(TaskItem task);
}
