using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto;
using MediatR;

namespace ExpTracker.Core.Transactions.Queries.GetTransaction
{
    public class GetTransactionQuery : IRequest<ServiceResponse<TransactionDto>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public GetTransactionQuery(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}