using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto;
using ExpTracker.Entities.Dto.Requests.Transactions;
using MediatR;

namespace ExpTracker.Core.Transactions.Commands.Create
{
    public class CreateTransactionCommand : IRequest<ServiceResponse<TransactionDto>>
    {
        public CreateTransactionRequest Request { get; set; }
        public Guid UserId { get; set; }

        public CreateTransactionCommand(CreateTransactionRequest request, Guid userId)
        {
            Request = request;
            UserId = userId;
        }
    }
}
