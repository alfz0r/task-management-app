using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TaskManagement.Primitives;
using TaskManagement.Primitives.DTOs;
using TaskManagement.Primitives.Interfaces.Repositories;
using TaskManagement.Primitives.Interfaces.Services;

namespace TaskManagement.Core.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _repository;
	private readonly AuthService _authService;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public UserService(IUserRepository repository, AuthService authService, IHttpContextAccessor httpContextAccessor, IMapper mapper) {
		_repository = repository;
		_authService = authService;
		_httpContextAccessor = httpContextAccessor;
		_mapper = mapper;
	}

	public async Task<string> AuthenticateUserAsync(string username, string password) {
		var user = await _repository.GetUserByUsernameAsync(username);
		if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
			throw new UnauthorizedAccessException("Invalid credentials");

		return _authService.GenerateToken(user);
	}

	public async Task RegisterUserAsync(string username, string password) {
		if (await _repository.GetUserByUsernameAsync(username) != null)
			throw new InvalidOperationException("Username already exists");

		var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
		await _repository.AddUserAsync(new User { Username = username, PasswordHash = hashedPassword });
	}

	public async Task<UserDto> GetCurrentUserAsync() {
		var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("id");

		if (userIdClaim == null) {
			throw new UnauthorizedAccessException("User is not authenticated");
		}

		var userId = int.Parse(userIdClaim.Value);

		var user = await _repository.GetUserByIdAsync(userId);
		if (user == null) {
			throw new UnauthorizedAccessException("User not found");
		}

		return _mapper.Map<UserDto>(user);
	}

	public async Task ChangePasswordAsync(int userId, string currentPassword, string newPassword) {
		var user = await _repository.GetUserByIdAsync(userId);
		if (user == null) {
			throw new UnauthorizedAccessException("User not found");
		}

		// Verify current password
		if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash)) {
			throw new UnauthorizedAccessException("Current password is incorrect");
		}

		// Hash and update the new password
		user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
		await _repository.UpdateUserAsync(user);
	}

	public async Task<IEnumerable<UserDto>> GetUsersAsync() {
		var users = await _repository.GetUsersAsync();
		return _mapper.Map<IEnumerable<UserDto>>(users);
	}
}
