using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Responses.Categories;
using ExpTracker.EntitiesMapping.Categories;
using MediatR;

namespace ExpTracker.Core.Categories.Commands.Create
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ServiceResponse<CreateCategoryResponse>>
    {
        private readonly ICategoriesRepository _repository;
        private readonly ITransactionCategoriesMapper _mapper;

        public CreateCategoryHandler(ICategoriesRepository repository, ITransactionCategoriesMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _repository.GetByNameAndUserIdAsync(request.Request.Name, request.UserId);

            if (existingCategory != null) return ServiceResponse<CreateCategoryResponse>.BadRequest($"Категория {request.Request.Name} уже существует");

            var mappedCategory = _mapper.Map(request.Request, request.UserId);

            var result = await _repository.CreateAsync(mappedCategory);

            return ServiceResponse<CreateCategoryResponse>.Ok(new() { Name = result.Name, Id = result.Id });
        }
    }
}
