using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Primitives;
using TaskManagement.Primitives.Interfaces.Services;

namespace TaskManagement.Web.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TaskController : ControllerBase
{
	private readonly ITaskService _service;

	public TaskController(ITaskService service) {
		_service = service;
	}

	[HttpGet]
	public async Task<IActionResult> GetTasks([FromQuery] TaskQueryParams queryParams) {
		var tasks = await _service.GetTasksAsync(queryParams);
		return Ok(tasks);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetTask(Guid id) {
		var task = await _service.GetTaskByIdAsync(id);
		if (task == null)
			return NotFound();
		return Ok(task);
	}

	[HttpPost]
	public async Task<IActionResult> AddTask([FromBody] TaskRequest task) {
		await _service.AddTaskAsync(task.Name);
		return Ok();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskRequest taskRequest) {
		if (id != taskRequest.Id)
			return BadRequest();
		await _service.UpdateTaskAsync(id, taskRequest);
		return Ok();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteTask(Guid id) {
		await _service.DeleteTaskAsync(id);
		return Ok();
	}
}
