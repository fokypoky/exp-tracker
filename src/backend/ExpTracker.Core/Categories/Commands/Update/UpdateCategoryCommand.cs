using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto;
using MediatR;

namespace ExpTracker.Core.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest<ServiceResponse<TransactionCategoryDto>>
    {
        public TransactionCategoryDto Request { get; set; }
        public Guid UserId { get; set; }

        public UpdateCategoryCommand(Guid userId, TransactionCategoryDto request)
        {
            Request = request;
            UserId = userId;
        }
    }
}
