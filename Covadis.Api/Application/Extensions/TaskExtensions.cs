using Covadis.Api.Application.DTOs.Task;

namespace Covadis.Api.Application.Extensions;

using TaskItem = Covadis.Api.Models.Task; 

public static class TaskExtensions
{
    public static TaskReadDto ToReadDto(this TaskItem task)
    {
        return new TaskReadDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            DueDate = task.DueDate,
            TeamId = task.TeamId,
            Team = task.Team
        };
    }
    
    public static TaskListDto ToListDto(this TaskItem task)
    {
        return new TaskListDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            DueDate = task.DueDate,
            TeamId = task.TeamId,
        };
    }
}