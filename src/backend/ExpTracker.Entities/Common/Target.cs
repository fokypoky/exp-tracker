namespace ExpTracker.Entities.Common;

public class Target
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime EndTime { get; set; }
    public decimal TargetCost { get; set; }
    public decimal CurrentAmount { get; set; }
    
    public User User { get; set; }
}