﻿namespace TaskManagement.Primitives.Requests;

public class ChangePasswordRequest
{
	public string CurrentPassword {
		get; set;
	}
	public string NewPassword {
		get; set;
	}
}