using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using MediatR;

namespace ExpTracker.Core.Categories.Commands.Update
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, ServiceResponse<TransactionCategoryDto>>
    {
        private readonly ICategoriesRepository _repository;

        public UpdateCategoryHandler(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<TransactionCategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var isExists = await _repository.IsExistsAsync(request.Request.Id, request.UserId);

            if (!isExists) return ServiceResponse<TransactionCategoryDto>.NotFound("Категория не найдена");

            var category = request.Request;
            var mappedCategory = new TransactionCategory()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                UserId = request.UserId
            };

            var result = await _repository.UpdateAsync(mappedCategory);

            var mappedResult = new TransactionCategoryDto()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
            };

            return ServiceResponse<TransactionCategoryDto>.Ok(mappedResult);
        }
    }
}
