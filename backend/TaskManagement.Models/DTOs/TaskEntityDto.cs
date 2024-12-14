namespace TaskManagement.Primitives.DTOs;

public class TaskEntityDto
{
	public Guid Id {
		get; set;
	}
	public int UserId {
		get; set;
	}
	public string Name {
		get; set;
	}
	public bool IsCompleted {
		get; set;
	}
	public string CreatedBy {
		get; set;
	}
	public string? ModifiedBy {
		get; set;
	}
	public DateTime CreatedAt {
		get; set;
	}
	public DateTime ModifiedAt {
		get; set;
	}
}
