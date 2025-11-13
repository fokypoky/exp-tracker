using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto;
using MediatR;

namespace ExpTracker.Core.Categories.Queries.GetCategory
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, ServiceResponse<TransactionCategoryDto>>
    {
        private readonly ICategoriesRepository _repository;

        public GetCategoryHandler(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<TransactionCategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(request.CategoryId);

            if (category == null)
                return ServiceResponse<TransactionCategoryDto>.NotFound("Категория не найдена");

            return ServiceResponse<TransactionCategoryDto>.Ok(new TransactionCategoryDto()
            {
                Id = category.Id,
                Name = category.Name
            });
        }
    }
}
