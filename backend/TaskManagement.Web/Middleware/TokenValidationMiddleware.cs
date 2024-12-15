public class TokenValidationMiddleware
{
	private readonly RequestDelegate _next;
	private readonly TokenService _tokenService;

	public TokenValidationMiddleware(RequestDelegate next, TokenService tokenService) {
		_next = next;
		_tokenService = tokenService;
	}

	public async Task InvokeAsync(HttpContext context) {
		var authHeader = context.Request.Headers["Authorization"].ToString();
		if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) {
			var token = authHeader.Substring("Bearer ".Length).Trim();
			if (!_tokenService.IsTokenValid(token)) {
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Invalid token.");
				return;
			}
		}

		await _next(context);
	}
}
