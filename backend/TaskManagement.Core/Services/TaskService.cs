using AutoMapper;
using TaskManagement.Primitives;
using TaskManagement.Primitives.DTOs;
using TaskManagement.Primitives.Interfaces.Repositories;
using TaskManagement.Primitives.Interfaces.Services;

namespace TaskManagement.Core.Services;

public class TaskService : ITaskService
{
	private readonly ITaskRepository _repository;
	private readonly IUserService _userService;
	private readonly IMapper _mapper;

	public TaskService(ITaskRepository repository, IUserService userService, IMapper mapper) {
		_repository = repository;
		_userService = userService;
		_mapper = mapper;
	}

	public async Task<IEnumerable<TaskEntityDto>> GetTasksAsync(TaskQueryParams queryParams) {
		var tasks = await _repository.GetTasksAsync(queryParams);
		return _mapper.Map<IEnumerable<TaskEntityDto>>(tasks);
	}

	public async Task<TaskEntityDto> GetTaskByIdAsync(Guid id) {
		var task = await _repository.GetTaskByIdAsync(id);
		return _mapper.Map<TaskEntityDto>(task);
	}

	public async Task AddTaskAsync(string taskName) {
		var user = await _userService.GetCurrentUserAsync();
		var task = new TaskEntity {
			Name = taskName,
			Id = Guid.NewGuid(),
			IsCompleted = false,
			CreatedBy = user.Username,
			CreatedAt = DateTime.UtcNow,
			ModifiedBy = null,
			ModifiedAt = null,
			UserId = user.Id
		};
		await _repository.AddTaskAsync(task);
	}

	public async Task UpdateTaskAsync(Guid taskId, TaskRequest taskRequest) {
		var user = await _userService.GetCurrentUserAsync();
		var task = await _repository.GetTaskByIdAsync(taskRequest.Id);

		if (task == null) {
			throw new KeyNotFoundException("Task not found");
		}

		task.IsCompleted = taskRequest.IsCompleted;
		task.Name = taskRequest.Name ?? task.Name;
		task.ModifiedBy = user.Username;
		task.ModifiedAt = DateTime.UtcNow;

		await _repository.UpdateTaskAsync(task);
	}

	public async Task DeleteTaskAsync(Guid id) {
		await _repository.DeleteTaskAsync(id);
	}
}
