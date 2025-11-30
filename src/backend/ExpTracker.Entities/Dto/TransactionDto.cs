using ExpTracker.Entities.Common;

namespace ExpTracker.Entities.Dto
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public TransactionType Type { get; set; }
        public TransactionIntervalType IntervalType { get; set; }
        public TransactionIntervalStrategy? IntervalStrategy { get; set; }
        public DateTime Date { get; set; }
        public int? DayOfMonth { get; set; }
        public string? Description { get; set; }
        public TransactionCategoryDto Category { get; set; }
    }
}
