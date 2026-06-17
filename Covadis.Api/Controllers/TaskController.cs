using Covadis.Api.Application.DTOs.Task;
using Covadis.Api.Application.Interfaces;
using Covadis.Api.Application.Services;
using Covadis.Api.Generics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Covadis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaskReadDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<ActionResult> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
                return NotFound(ApiResponse<string>.Fail("Taak niet gevonden."));

            return Ok(ApiResponse<TaskReadDto>.Ok(task, "Taak succesvol opgehaald."));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TaskReadDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public async Task<ActionResult> CreateTaskAsync([FromBody] TaskCreateDto taskCreateDto)
        {
            var task = await _taskService.CreateAsync(taskCreateDto);

            if (task == null)
                return BadRequest(ApiResponse<string>.Fail("Taak kon niet worden aangemaakt."));

            return Ok(ApiResponse<TaskReadDto>.Ok(task));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaskReadDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<ActionResult> UpdateTaskAsync(Guid id, [FromBody] TaskUpdateDto taskUpdateDto)
        {
            var task = await _taskService.UpdateAsync(id, taskUpdateDto);
            
            if (task == null)
                return NotFound(ApiResponse<string>.Fail("Taak niet gevonden."));
            
            return Ok(ApiResponse<TaskReadDto>.Ok(task, "Taak succesvol bijgewerkt."));
        }
    }
}
