namespace ExpTracker.Entities.Common;

public class TransactionCategory
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    
    public User User { get; set; }
    public List<Transaction> Transactions { get; set; }
}