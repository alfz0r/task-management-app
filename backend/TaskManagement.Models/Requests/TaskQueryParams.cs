namespace TaskManagement.Primitives;

public class TaskQueryParams
{
	public string? SortBy {
		get; set;
	}
	public bool IsAscending { get; set; } = true;
	public bool? IsCompleted {
		get; set;
	}
	public int? UserId {
		get; set;
	}
}
