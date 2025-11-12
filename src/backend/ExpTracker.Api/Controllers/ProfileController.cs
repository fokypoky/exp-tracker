using System.Diagnostics.CodeAnalysis;
using ExpTracker.Api.Extensions;
using ExpTracker.Api.Mapping.ClaimsParser;
using ExpTracker.Core.Profile.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExcludeFromCodeCoverage]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var user = ClaimsParser.GetUser(this.User);

            var response = await _mediator.Send(new GetProfileQuery(user));
            
            return this.MapResponse(response);
        }
    }
}