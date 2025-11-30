using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto;
using ExpTracker.EntitiesMapping.Categories;
using MediatR;

namespace ExpTracker.Core.Categories.Queries.GetCategories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, ServiceResponse<PaginatedResponse<TransactionCategoryDto>>>
    {
        private readonly ICategoriesRepository _repository;
        private readonly ITransactionCategoriesMapper _mapper;

        public GetCategoriesHandler(ICategoriesRepository repository, ITransactionCategoriesMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PaginatedResponse<TransactionCategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetRangeAsync(request.UserId, request.Request.Limit, request.Request.Offset, request.Request.Search);
            return ServiceResponse<TransactionCategoryDto>.Partial(
                result.Data.Select(e => _mapper.Map(e)).ToList(),
                result.TotalCount
            );
        }
    }
}
