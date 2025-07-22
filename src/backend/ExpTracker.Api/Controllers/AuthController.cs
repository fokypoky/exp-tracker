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
			throw new NotImplementedException();
		}

		[HttpPost("logout")]
		public async Task<IResult> LogOut(RefreshTokenRequest request)
		{
			throw new NotImplementedException();
		}

		[HttpPost("refresh")]
		public async Task<IResult> Refresh(RefreshTokenRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
