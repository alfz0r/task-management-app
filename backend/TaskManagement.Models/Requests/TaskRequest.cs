using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Primitives;

public class TaskRequest
{
	public Guid Id {
		get; set;
	}
	public string Name {
		get; set;
	}
	public bool IsCompleted {
		get; set;
	}
}
