using ExpTracker.Entities.Common;

namespace ExpTracker.Entities.Dto.Requests.Transactions
{
    public class CreateTransactionRequest
    {
        public decimal Cost { get; set; }
        public TransactionType Type { get; set; }
        public TransactionIntervalType IntervalType { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
