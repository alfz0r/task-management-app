using Microsoft.EntityFrameworkCore;
using TaskManagement.Primitives;
using TaskManagement.Primitives.Interfaces.Repositories;

namespace TaskManagement.Data.Repositories;

public class TaskRepository : ITaskRepository
{
	private readonly ApplicationDbContext _context;

	public TaskRepository(ApplicationDbContext context) {
		_context = context;
	}

	public async Task<IEnumerable<TaskEntity>> GetTasksAsync(TaskQueryParams queryParams) {
		var query = _context.Tasks.AsQueryable();

		if (queryParams.UserId.HasValue) {
			query = query.Where(t => t.UserId == queryParams.UserId.Value);
		}

		if (queryParams.IsCompleted.HasValue) {
			query = query.Where(t => t.IsCompleted == queryParams.IsCompleted.Value);
		}

		if (!string.IsNullOrWhiteSpace(queryParams.SortBy)) {
			query = queryParams.SortBy.ToLower() switch {
				"name" => queryParams.IsAscending ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name),
				"createdat" => queryParams.IsAscending ? query.OrderBy(t => t.CreatedAt) : query.OrderByDescending(t => t.CreatedAt),
				_ => query
			};
		}

		return await query.ToListAsync();
	}

	public async Task<TaskEntity> GetTaskByIdAsync(Guid id) {
		return await _context.Tasks.FindAsync(id);
	}

	public async Task AddTaskAsync(TaskEntity task) {
		_context.Tasks.Add(task);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateTaskAsync(TaskEntity task) {
		_context.Tasks.Update(task);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteTaskAsync(Guid id) {
		var task = await _context.Tasks.FindAsync(id);
		if (task != null) {
			_context.Tasks.Remove(task);
			await _context.SaveChangesAsync();
		}
	}
}
