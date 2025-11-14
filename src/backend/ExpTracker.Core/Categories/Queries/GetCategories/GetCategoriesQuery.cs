using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto;
using ExpTracker.Entities.Dto.Requests.Categories;
using ExpTracker.Entities.Dto.Responses.Categories;
using MediatR;

namespace ExpTracker.Core.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<ServiceResponse<PaginatedResponse<TransactionCategoryDto>>>
    {
        public GetCategoriesRequest Request { get; set; }
        public Guid UserId { get; set; }

        public GetCategoriesQuery(Guid userId, GetCategoriesRequest request)
        {
            Request = request;
            UserId = userId;
        }
    }
}
