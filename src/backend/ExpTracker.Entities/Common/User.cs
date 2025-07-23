namespace ExpTracker.Entities.Common;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string? RefreshToken { get; set; }
    
    public List<Target> Targets { get; set; }
    public List<TransactionCategory> TransactionCategories { get; set; }
    public List<Transaction> Transactions { get; set; }
}