using System.Diagnostics.CodeAnalysis;
using ExpTracker.Api.Extensions;
using ExpTracker.Api.Mapping.ClaimsParser;
using ExpTracker.Core.Transactions.Commands.Create;
using ExpTracker.Core.Transactions.Commands.Delete;
using ExpTracker.Core.Transactions.Queries.GetTransactions;
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
        public async Task<IActionResult> Get([FromQuery] GetTransactionsRequest request)
        {
            var userId = ClaimsParser.GetUserId(this.User);

            var result = await _mediator.Send(new GetTransactionsQuery(request, userId));

            return this.MapPaginatedResponse(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request)
        {
            var userId = ClaimsParser.GetUserId(this.User);

            var result = await _mediator.Send(new CreateTransactionCommand(request, userId));

            return this.MapResponse(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userId = ClaimsParser.GetUserId(this.User);

            var result = await _mediator.Send(new DeleteTransactionCommand(id, userId));

            return this.MapResponse(result);
        }
    }
}
