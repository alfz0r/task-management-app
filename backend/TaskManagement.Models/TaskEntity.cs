namespace TaskManagement.Primitives;

public class TaskEntity
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
	public DateTime? ModifiedAt {
		get; set; 
	}

	public User? User {
		get; set;
	}
}
