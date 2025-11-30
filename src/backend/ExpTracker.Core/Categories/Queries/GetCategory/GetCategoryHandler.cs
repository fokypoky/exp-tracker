using AutoMapper;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto;
using ExpTracker.EntitiesMapping.Categories;
using MediatR;

namespace ExpTracker.Core.Categories.Queries.GetCategory
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, ServiceResponse<TransactionCategoryDto>>
    {
        private readonly ICategoriesRepository _repository;
        private readonly ITransactionCategoriesMapper _mapper;

        public GetCategoryHandler(ICategoriesRepository repository, ITransactionCategoriesMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TransactionCategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAndUserIdAsync(request.CategoryId, request.UserId);

            if (category == null)
                return ServiceResponse<TransactionCategoryDto>.NotFound("Категория не найдена");

            TransactionCategoryDto mappedDto = _mapper.Map(category);

            return ServiceResponse<TransactionCategoryDto>.Ok(mappedDto);
        }
    }
}
