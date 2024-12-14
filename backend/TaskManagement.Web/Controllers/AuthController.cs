using Microsoft.AspNetCore.Mvc;
using TaskManagement.Primitives.Interfaces.Services;
using TaskManagement.Primitives;

namespace TaskManagement.Web.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
	private readonly IUserService _userService;

	public AuthController(IUserService userService) {
		_userService = userService;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] AuthRequest request) {
		try {
			var token = await _userService.AuthenticateUserAsync(request.Username, request.Password);
			return Ok(new {
				Token = token
			});
		} catch {
			return Unauthorized("Invalid credentials");
		}
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] AuthRequest request) {
		try {
			await _userService.RegisterUserAsync(request.Username, request.Password);
			return Ok("User registered successfully");
		} catch (InvalidOperationException ex) {
			return BadRequest(ex.Message);
		}
	}
}
