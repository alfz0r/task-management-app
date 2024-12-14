using TaskManagement.Primitives.DTOs;

namespace TaskManagement.Primitives.Interfaces.Services;

public interface IUserService
{
	Task<string> AuthenticateUserAsync(string username, string password);
	Task RegisterUserAsync(string username, string password);
	Task<UserDto> GetCurrentUserAsync();
	Task ChangePasswordAsync(int userId, string currentPassword, string newPassword);
	Task<IEnumerable<UserDto>> GetUsersAsync();
}
