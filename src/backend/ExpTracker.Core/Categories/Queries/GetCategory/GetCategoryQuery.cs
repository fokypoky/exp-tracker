using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto;
using MediatR;

namespace ExpTracker.Core.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<ServiceResponse<TransactionCategoryDto>>
    {
        public Guid CategoryId { get; set; }
        
        public GetCategoryQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
