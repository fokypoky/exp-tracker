using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto;
using ExpTracker.EntitiesMapping.Categories;
using MediatR;

namespace ExpTracker.Core.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, ServiceResponse<List<TransactionCategoryDto>>>
    {
        private readonly ICategoriesRepository _repository;
        private readonly ITransactionCategoriesMapper _mapper;

        public GetAllCategoriesHandler(ICategoriesRepository repository, ITransactionCategoriesMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<TransactionCategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetRangeAsync(request.UserId);
            var mappedResult = categories.Select(x => _mapper.Map(x)).ToList();
            
            return ServiceResponse<List<TransactionCategoryDto>>.Ok(mappedResult);
        }
    }
}
