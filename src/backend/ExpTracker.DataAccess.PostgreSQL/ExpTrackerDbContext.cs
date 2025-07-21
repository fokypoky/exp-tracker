using ExpTracker.DataAccess.PostgreSQL.Configurations;
using ExpTracker.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ExpTracker.DataAccess.PostgreSQL;

public class ExpTrackerDbContext(DbContextOptions<ExpTrackerDbContext> options) : DbContext(options)
{
    public DbSet<Target> Targets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionCategory> TransactionCategories { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TargetsConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionsConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionCategoriesConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
    }
}