using System.Diagnostics.CodeAnalysis;
using ExpTracker.Api.Extensions;
using ExpTracker.Api.Mapping.ClaimsParser;
using ExpTracker.Core.Transactions.Commands.Create;
using ExpTracker.Entities.Dto.Requests.Transactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExcludeFromCodeCoverage]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request)
        {
            var userId = ClaimsParser.GetUserId(this.User);

            var result = await _mediator.Send(new CreateTransactionCommand(request, userId));

            return this.MapResponse(result);
        }
    }
}
