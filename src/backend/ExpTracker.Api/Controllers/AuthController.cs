using ExpTracker.Api.Extensions;
using ExpTracker.Core.Auth.Commands.Register;
using ExpTracker.Core.Interfaces;
using ExpTracker.Entities.Dto.Requests.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
        private readonly IMediator _mediator;

		public AuthController(IAuthService authService, IMediator mediator)
		{
			_authService = authService;
			_mediator = mediator;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] AuthRequest request)
		{
            var response = await _mediator.Send(new RegisterCommand(request));
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
