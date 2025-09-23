using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Requests.Categories;
using ExpTracker.Entities.Dto.Responses.Categories;

namespace ExpTracker.Core.Implementation
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _repository;

        public CategoriesService(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<CreateCategoryResponse>> CreateAsync(Guid userId, CreateCategoryRequest request)
        {
            var existingCategory = await _repository.GetByNameAndUserIdAsync(request.Name, userId);

            if (existingCategory != null) return ServiceResponse<CreateCategoryResponse>.BadRequest($"Category {request.Name} already exists");

            var category = new TransactionCategory { Name = request.Name, UserId = userId };

            var result = await _repository.CreateAsync(category);

            return ServiceResponse<CreateCategoryResponse>.Ok(new() { Name = result.Name, Id = result.Id });
        }

        public async Task<ServiceResponse<PaginatedResponse<GetCategoryResponse>>> GetAsync(Guid userId, GetCategoriesRequest request)
        {
            var result = await _repository.GetRangeAsync(userId, request.Limit, request.Offset);

            return ServiceResponse<GetCategoryResponse>.Partial(
                result.Data.Select(_ => new GetCategoryResponse() { Id = _.Id, Name = _.Name }).ToList(),
                result.TotalCount
            );
        }
    }
}
