using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto;
using MediatR;

namespace ExpTracker.Core.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<ServiceResponse<List<TransactionCategoryDto>>>
    {
        public Guid UserId { get; set; }

        public GetAllCategoriesQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
