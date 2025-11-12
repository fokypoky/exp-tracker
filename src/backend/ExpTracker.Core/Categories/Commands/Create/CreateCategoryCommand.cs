using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Requests.Categories;
using ExpTracker.Entities.Dto.Responses.Categories;
using MediatR;

namespace ExpTracker.Core.Categories.Commands.Create
{
    public class CreateCategoryCommand : IRequest<ServiceResponse<CreateCategoryResponse>>
    {
        public CreateCategoryRequest Request { get; set; }
        public Guid UserId { get; set; }

        public CreateCategoryCommand(Guid userId, CreateCategoryRequest request)
        {
            Request = request;
            UserId = userId;
        }
    }
}
