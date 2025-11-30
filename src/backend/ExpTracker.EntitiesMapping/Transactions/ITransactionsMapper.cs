using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using ExpTracker.Entities.Dto.Requests.Transactions;

namespace ExpTracker.EntitiesMapping.Transactions
{
    public interface ITransactionsMapper
    {
        Transaction Map(TransactionDto dto, Guid userId);
        Transaction Map(CreateTransactionRequest request, Guid userId);
        TransactionDto Map(Transaction transaction);
    }
}
