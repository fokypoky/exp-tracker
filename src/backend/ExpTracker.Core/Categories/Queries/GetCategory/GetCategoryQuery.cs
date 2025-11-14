using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto;
using MediatR;

namespace ExpTracker.Core.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<ServiceResponse<TransactionCategoryDto>>
    {
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        
        public GetCategoryQuery(Guid categoryId, Guid userId)
        {
            CategoryId = categoryId;
            UserId = userId;
        }
    }
}
