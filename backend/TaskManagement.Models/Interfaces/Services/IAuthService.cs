namespace TaskManagement.Primitives.Interfaces.Services;

public interface IAuthService
{
	Task<string> AuthenticateUserAsync(string username, string password);
	Task RegisterUserAsync(string username, string password);
}
