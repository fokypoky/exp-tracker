using ExpTracker.Api.Extensions;
using ExpTracker.Api.Mapping.ResponseMapper;
using ExpTracker.Core.Interfaces;
using ExpTracker.Entities.Dto.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] AuthRequest request)
		{
			var response = await _authService.Register(request);
			return this.MapResponse(response);
		}

		[HttpPost("login")]
		public async Task<IActionResult> LogIn([FromBody] AuthRequest request)
		{
			var response = await _authService.LogIn(request);
			return this.MapResponse(response);
		}

		[HttpPost("logout")]
		public async Task<IActionResult> LogOut([FromBody] RefreshTokenRequest request)
		{
			var response = await _authService.LogOut(request);
			return this.MapResponse(response);
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
		{
			var response = await _authService.RefreshToken(request);
			return this.MapResponse(response);
		}
	}
}
