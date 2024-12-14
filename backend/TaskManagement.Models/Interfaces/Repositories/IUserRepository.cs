namespace TaskManagement.Primitives.Interfaces.Repositories;

public interface IUserRepository
{
	Task<User> GetUserByUsernameAsync(string username);
	Task AddUserAsync(User user);
	Task<User> GetUserByIdAsync(int id);
	Task UpdateUserAsync(User user);
	Task<IEnumerable<User>> GetUsersAsync();
}
