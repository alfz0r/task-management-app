namespace TaskManagement.Primitives.Interfaces.Repositories;

public interface ITaskRepository
{
	Task<IEnumerable<TaskEntity>> GetTasksAsync(TaskQueryParams queryParams);
	Task<TaskEntity> GetTaskByIdAsync(Guid id);
	Task AddTaskAsync(TaskEntity task);
	Task UpdateTaskAsync(TaskEntity task);
	Task DeleteTaskAsync(Guid id);
}
