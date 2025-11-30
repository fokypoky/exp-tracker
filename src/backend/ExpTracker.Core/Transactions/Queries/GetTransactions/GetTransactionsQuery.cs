using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto;
using ExpTracker.Entities.Dto.Requests.Transactions;
using MediatR;

namespace ExpTracker.Core.Transactions.Queries.GetTransactions
{
    public class GetTransactionsQuery : IRequest<ServiceResponse<PaginatedResponse<TransactionDto>>>
    {
        public GetTransactionsRequest Request { get; set; }
        public Guid UserId { get; set; }

        public GetTransactionsQuery(GetTransactionsRequest request, Guid userId)
        {
            Request = request;
            UserId = userId;
        }
    }
}
