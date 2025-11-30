namespace ExpTracker.Entities.Common
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Cost { get; set; }
        public Guid CategoryId { get; set; }
        public TransactionType Type { get; set; }
        public TransactionIntervalType IntervalType { get; set; }
        public TransactionIntervalStrategy? IntervalStrategy { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public TransactionCategory Category { get; set; }
    }
}