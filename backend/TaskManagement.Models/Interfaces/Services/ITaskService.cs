using TaskManagement.Primitives.DTOs;

namespace TaskManagement.Primitives.Interfaces.Services;

public interface ITaskService
{
	Task<IEnumerable<TaskEntityDto>> GetTasksAsync(TaskQueryParams queryParams);
	Task<TaskEntityDto> GetTaskByIdAsync(Guid id);
	Task AddTaskAsync(string taskName);
	Task UpdateTaskAsync(Guid id, TaskRequest taskRequest);
	Task DeleteTaskAsync(Guid id);
}

