using ExpTracker.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpTracker.DataAccess.PostgreSQL.Configurations;

public class TransactionCategoriesConfiguration : IEntityTypeConfiguration<TransactionCategory>
{
    public void Configure(EntityTypeBuilder<TransactionCategory> builder)
    {
        builder.ToTable("transaction_categories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasOne(tc => tc.User)
            .WithMany(u => u.TransactionCategories);
    }
}