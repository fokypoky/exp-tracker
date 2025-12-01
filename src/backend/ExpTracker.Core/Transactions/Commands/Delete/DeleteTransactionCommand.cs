using ExpTracker.Core.Models;
using MediatR;

namespace ExpTracker.Core.Transactions.Commands.Delete
{
    public class DeleteTransactionCommand : IRequest<ServiceResponse<object>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public DeleteTransactionCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
