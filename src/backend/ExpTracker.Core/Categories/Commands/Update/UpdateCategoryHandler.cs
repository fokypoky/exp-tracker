using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto;
using ExpTracker.EntitiesMapping.Categories;
using MediatR;

namespace ExpTracker.Core.Categories.Commands.Update
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, ServiceResponse<TransactionCategoryDto>>
    {
        private readonly ICategoriesRepository _repository;
        private readonly ITransactionCategoriesMapper _mapper;

        public UpdateCategoryHandler(ICategoriesRepository repository, ITransactionCategoriesMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TransactionCategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var isExists = await _repository.IsExistsAsync(request.Request.Id, request.UserId);
            var newNameCategory = await _repository.GetByUserIdAndNameAsync(request.UserId, request.Request.Name);

            if (!isExists) return ServiceResponse<TransactionCategoryDto>.NotFound("Категория не найдена");
            if (newNameCategory != null) return ServiceResponse<TransactionCategoryDto>.BadRequest("Категория уже существует");

            var mappedCategory = _mapper.Map(request.Request, request.UserId);

            var result = await _repository.UpdateAsync(mappedCategory);

            var mappedResult = _mapper.Map(result);

            return ServiceResponse<TransactionCategoryDto>.Ok(mappedResult);
        }
    }
}
