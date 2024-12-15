using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Primitives.Interfaces.Configuration;
using TaskManagement.Primitives;

public class TokenService
{
	private readonly string _key;
	private readonly HashSet<string> _invalidatedTokens = new();

	public TokenService(IOptions<JwtOptions> options) {
		_key = options.Value.Key;
	}

	public string GenerateToken(User user) {
		var handler = new JwtSecurityTokenHandler();
		var privateKey = Encoding.UTF8.GetBytes(_key);

		var credentials = new SigningCredentials(
			new SymmetricSecurityKey(privateKey),
			SecurityAlgorithms.HmacSha256);

		var tokenDescriptor = new SecurityTokenDescriptor {
			SigningCredentials = credentials,
			Expires = DateTime.UtcNow.AddHours(1),
			Subject = GenerateClaims(user)
		};

		var token = handler.CreateToken(tokenDescriptor);
		return handler.WriteToken(token);
	}

	private static ClaimsIdentity GenerateClaims(User user) {
		var ci = new ClaimsIdentity();

		ci.AddClaim(new Claim("id", user.Id.ToString()));
		ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));

		return ci;
	}

	public bool IsTokenValid(string token) {
		if (string.IsNullOrWhiteSpace(token))
			return false;

		var handler = new JwtSecurityTokenHandler();
		var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

		if (jwtToken == null)
			return false;

		return !_invalidatedTokens.Contains(token);
	}

	public void InvalidateToken(string token) {
		_invalidatedTokens.Add(token);
	}
}
