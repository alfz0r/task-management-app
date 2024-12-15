using Microsoft.AspNetCore.Mvc;
using TaskManagement.Primitives.Interfaces.Services;
using TaskManagement.Primitives;
using TaskManagement.Core.Services;

namespace TaskManagement.Web.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;
	private readonly TokenService _tokenService;

	public AuthController(IAuthService authService, TokenService tokenService) {
		_authService = authService;
		_tokenService = tokenService;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] AuthRequest request) {
		try {
			var token = await _authService.AuthenticateUserAsync(request.Username, request.Password);
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
			await _authService.RegisterUserAsync(request.Username, request.Password);
			return Ok("User registered successfully");
		} catch (InvalidOperationException ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("logout")]
	public IActionResult Logout() {
		var authHeader = Request.Headers["Authorization"].ToString();
		if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ")) {
			return BadRequest("Invalid token.");
		}

		var token = authHeader.Replace("Bearer ", "");
		_tokenService.InvalidateToken(token);

		return Ok("Logged out successfully.");
	}
}
