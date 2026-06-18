using Covadis.Api.Application.DTOs.Task;

namespace Covadis.Api.Application.Interfaces;

public interface ITaskService
{
    Task<TaskReadDto?> GetByIdAsync(Guid taskId);
    Task<TaskReadDto?> UpdateAsync(Guid taskId, TaskUpdateDto dto);
    Task<TaskReadDto> CreateAsync(TaskCreateDto dto);
}