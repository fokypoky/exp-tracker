using System.Diagnostics.CodeAnalysis;
using ExpTracker.Api.Extensions;
using ExpTracker.Api.Mapping.ClaimsParser;
using ExpTracker.Core.Categories.Commands.Create;
using ExpTracker.Core.Categories.Queries.GetCategories;
using ExpTracker.Core.Categories.Queries.GetCategory;
using ExpTracker.Entities.Dto.Requests.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExcludeFromCodeCoverage]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var userId = ClaimsParser.GetUserId(this.User);

            var result = await _mediator.Send(new CreateCategoryCommand(userId, request));

            return this.MapResponse(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetCategoriesRequest request)
        {
            var userId = ClaimsParser.GetUserId(this.User);

            var result = await _mediator.Send(new GetCategoriesQuery(userId, request));

            return this.MapPaginatedResponse(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var userId = ClaimsParser.GetUserId(this.User);
            
            var result = await _mediator.Send(new GetCategoryQuery(id, userId));

            return this.MapResponse(result);
        }
    }
}
