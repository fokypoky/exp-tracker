using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto;
using MediatR;

namespace ExpTracker.Core.Categories.Queries.GetCategories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, ServiceResponse<PaginatedResponse<TransactionCategoryDto>>>
    {
        private readonly ICategoriesRepository _repository;

        public GetCategoriesHandler(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<PaginatedResponse<TransactionCategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetRangeAsync(request.UserId, request.Request.Limit, request.Request.Offset, request.Request.Search);

            return ServiceResponse<TransactionCategoryDto>.Partial(
                result.Data.Select(_ => new TransactionCategoryDto()
                {
                    Id = _.Id,
                    Name = _.Name,
                    Description = _.Description
                }).ToList(),
                result.TotalCount
            );
        }
    }
}
