using TaskManagement.Primitives.DTOs;

namespace TaskManagement.Primitives.Interfaces.Services;

public interface IUserService
{
	Task<UserDto> GetCurrentUserAsync();
	Task ChangePasswordAsync(int userId, string currentPassword, string newPassword);
	Task<IEnumerable<UserDto>> GetUsersAsync();
}
