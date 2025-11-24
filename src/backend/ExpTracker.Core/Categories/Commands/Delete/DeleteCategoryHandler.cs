using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using MediatR;

namespace ExpTracker.Core.Categories.Commands.Delete
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, ServiceResponse<bool?>>
    {
        private readonly ICategoriesRepository _repository;

        public DeleteCategoryHandler(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<bool?>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAndUserIdAsync(request.Id, request.UserId);

            if (category == null) return ServiceResponse<bool?>.NotFound("Категория не найдена");

            var isRelated = await _repository.IsRelatedAsync(request.Id);
            if (isRelated) return ServiceResponse<bool?>.BadRequest("У категории есть добавленные связи");

            await _repository.DeleteAsync(category);

            return ServiceResponse<bool?>.Created(true);
        }
    }
}
