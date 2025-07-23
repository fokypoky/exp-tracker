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
		public async Task<IResult> Register(AuthRequest request)
		{
			var response = await _authService.Register(request);
			return ResponseMapper.MapResponse(response);
		}

		[HttpPost("login")]
		public async Task<IResult> LogIn(AuthRequest request)
		{
			var response = await _authService.LogIn(request);
			return ResponseMapper.MapResponse(response);
		}

		[HttpPost("logout")]
		public async Task<IResult> LogOut(RefreshTokenRequest request)
		{
			var response = await _authService.LogOut(request);
			return ResponseMapper.MapResponse(response);
		}

		[HttpPost("refresh")]
		public async Task<IResult> Refresh(RefreshTokenRequest request)
		{
			var response = await _authService.RefreshToken(request);
			return ResponseMapper.MapResponse(response);
		}
	}
}
