using ExpTracker.Core.Models;
using MediatR;

namespace ExpTracker.Core.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<ServiceResponse<bool?>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public DeleteCategoryCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
