using ExpTracker.Api.Extensions;
using ExpTracker.Api.Mapping.ClaimsParser;
using ExpTracker.Core.Interfaces;
using ExpTracker.Entities.Dto.Requests.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var userId = ClaimsParser.GetUserId(this.User);

            var result = await _categoriesService.CreateAsync(userId, request);

            return this.MapResponse(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetCategoriesRequest request)
        {
            var userId = ClaimsParser.GetUserId(this.User);

            var result = await _categoriesService.GetAsync(userId, request);

            return this.MapPaginatedResponse(result);
        }
    }
}
