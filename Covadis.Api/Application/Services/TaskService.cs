using Covadis.Api.Application.DTOs.Task;
using Covadis.Api.Application.Extensions;
using Covadis.Api.Application.Interfaces;

namespace Covadis.Api.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    
    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    public async Task<TaskReadDto?> GetByIdAsync(Guid taskId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);
        
        if (task == null)
            return null;
        
        return task.ToReadDto();
    }
    
    public async Task<TaskReadDto?> UpdateAsync(Guid taskId, TaskUpdateDto dto)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);
        
        if (task == null)
            return null;
        
        task.Title = dto.Title ?? task.Title;
        task.Description = dto.Description ?? task.Description;
        task.DueDate = dto.DueDate ?? task.DueDate;
        task.Status = dto.Status ?? task.Status;
        
        await _taskRepository.UpdateTaskAsync(task);
        
        return task.ToReadDto();
    }

    public async Task<TaskReadDto> CreateAsync(TaskCreateDto dto)
    {
        var task = new Models.Task
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Status = dto.Status,
            SprintNumber = dto.SprintNumber,
            TeamId = dto.TeamId
        };

        await _taskRepository.UpdateTaskAsync(task);

        return task.ToReadDto();
    }
}