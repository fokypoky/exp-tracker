using System.Diagnostics.CodeAnalysis;
using ExpTracker.Api.Extensions;
using ExpTracker.Core.Auth.Commands.LogIn;
using ExpTracker.Core.Auth.Commands.LogOut;
using ExpTracker.Core.Auth.Commands.Refresh;
using ExpTracker.Core.Auth.Commands.Register;
using ExpTracker.Entities.Dto.Requests.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExcludeFromCodeCoverage]
    public class AuthController : ControllerBase
	{
        private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
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
            var response = await _mediator.Send(new LogInCommand(request));
			return this.MapResponse(response);
		}

		[HttpPost("logout")]
		public async Task<IActionResult> LogOut([FromBody] RefreshTokenRequest request)
        {
            var response = await _mediator.Send(new LogOutCommand(request));
			return this.MapResponse(response);
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
		{
            var response = await _mediator.Send(new RefreshTokenCommand(request));
			return this.MapResponse(response);
		}
	}
}
