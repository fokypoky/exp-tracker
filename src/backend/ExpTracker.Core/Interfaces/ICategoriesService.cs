using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Requests.Categories;
using ExpTracker.Entities.Dto.Responses.Categories;

namespace ExpTracker.Core.Interfaces
{
    public interface ICategoriesService
    {
        Task<ServiceResponse<CreateCategoryResponse>> CreateAsync(Guid userId, CreateCategoryRequest request);
        Task<ServiceResponse<PaginatedResponse<GetCategoryResponse>>> GetAsync(Guid userId, GetCategoriesRequest request);
    }
}
