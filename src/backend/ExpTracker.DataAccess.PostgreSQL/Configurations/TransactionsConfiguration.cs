using ExpTracker.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExpTracker.DataAccess.PostgreSQL.Configurations;

public class TransactionsConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(e => e.Cost)
            .HasColumnName("cost")
            .IsRequired();

        builder.Property(e => e.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        builder.Property(e => e.Type)
            .HasColumnName("type")
            .HasConversion(new EnumToStringConverter<TransactionType>())
            .IsRequired();

        builder.Property(e => e.IntervalType)
            .HasColumnName("interval_type")
            .HasConversion(new EnumToStringConverter<TransactionIntervalType>())
            .IsRequired();

        builder.Property(e => e.IntervalStrategy)
            .HasColumnName("interval_strategy")
            .HasConversion(new EnumToStringConverter<TransactionIntervalStrategy>())
            .IsRequired(false);

        builder.Property(e => e.Date)
            .HasColumnName("date")
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .IsRequired(false);

        builder.Property(e => e.DayOfMonth)
            .HasColumnName("day_of_month")
            .IsRequired(false);

        builder
            .HasOne(t => t.User)
            .WithMany(u => u.Transactions);
        
        builder
            .HasOne(t => t.Category)
            .WithMany(c => c.Transactions);

    }
}