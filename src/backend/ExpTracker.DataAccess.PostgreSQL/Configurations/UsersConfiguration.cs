using ExpTracker.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpTracker.DataAccess.PostgreSQL.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Login).IsUnique();

        builder.Property(e => e.Login)
            .HasColumnName("login")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.Password)
            .HasColumnName("password")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(e => e.RefreshToken)
            .HasColumnName("refresh_token")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .HasMany(u => u.TransactionCategories)
            .WithOne(c => c.User);

        builder
            .HasMany(u => u.Targets)
            .WithOne(t => t.User);
    }
}