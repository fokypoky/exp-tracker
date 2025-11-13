using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto.Responses.Categories;
using MediatR;

namespace ExpTracker.Core.Categories.Queries.GetCategories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, ServiceResponse<PaginatedResponse<GetCategoryResponse>>>
    {
        private readonly ICategoriesRepository _repository;

        public GetCategoriesHandler(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<PaginatedResponse<GetCategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetRangeAsync(request.UserId, request.Request.Limit, request.Request.Offset);

            return ServiceResponse<GetCategoryResponse>.Partial(
                result.Data.Select(_ => new GetCategoryResponse() { Id = _.Id, Name = _.Name }).ToList(),
                result.TotalCount
            );
        }
    }
}
