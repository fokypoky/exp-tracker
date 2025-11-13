using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Responses.Categories;
using MediatR;

namespace ExpTracker.Core.Categories.Commands.Create
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ServiceResponse<CreateCategoryResponse>>
    {
        private ICategoriesRepository _repository;

        public CreateCategoryHandler(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _repository.GetByNameAndUserIdAsync(request.Request.Name, request.UserId);

            if (existingCategory != null) return ServiceResponse<CreateCategoryResponse>.BadRequest($"Категория {request.Request.Name} уже существует");

            var category = new TransactionCategory { Name = request.Request.Name, UserId = request.UserId };

            var result = await _repository.CreateAsync(category);

            return ServiceResponse<CreateCategoryResponse>.Ok(new() { Name = result.Name, Id = result.Id });
        }
    }
}
