using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TaskManagement.Primitives;
using TaskManagement.Primitives.DTOs;
using TaskManagement.Primitives.Interfaces.Repositories;
using TaskManagement.Primitives.Interfaces.Services;

namespace TaskManagement.Core.Services;

public class AuthService : IAuthService
{
	private readonly IUserRepository _repository;
	private readonly TokenService _authService;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public AuthService(IUserRepository repository, TokenService authService, IHttpContextAccessor httpContextAccessor, IMapper mapper) {
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
}
