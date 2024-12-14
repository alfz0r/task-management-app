using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Primitives.Interfaces.Services;
using TaskManagement.Primitives.Requests;

namespace TaskManagement.Web.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;

	public UserController(IUserService userService) {
		_userService = userService;
	}

	[HttpGet("profile")]
	[Authorize]
	public async Task<IActionResult> GetProfile() {
		var user = await _userService.GetCurrentUserAsync();

		if (user == null)
			return NotFound("User not found");

		return Ok(new {
			username = user.Username
		});
	}

	[HttpPost("change-password")]
	[Authorize]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request) {
		var user = await _userService.GetCurrentUserAsync();

		try {
			await _userService.ChangePasswordAsync(user.Id, request.CurrentPassword, request.NewPassword);
			return Ok("Password changed successfully");
		} catch (UnauthorizedAccessException) {
			return BadRequest("Current password is incorrect");
		}
	}

	[HttpGet("all")]
	[Authorize]
	public async Task<IActionResult> GetAllUsers() {
		var users = await _userService.GetUsersAsync();

		return Ok(users);
	}

}
