using ExpTracker.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpTracker.DataAccess.PostgreSQL.Configurations;

public class TargetsConfiguration : IEntityTypeConfiguration<Target>
{
    public void Configure(EntityTypeBuilder<Target> builder)
    {
        builder.ToTable("targets");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("description");

        builder.Property(e => e.Created)
            .HasColumnName("created")
            .IsRequired();

        builder.Property(e => e.EndTime)
            .HasColumnName("end_time")
            .IsRequired();

        builder.Property(e => e.TargetCost)
            .HasColumnName("target_cost")
            .IsRequired();

        builder.Property(e => e.CurrentAmount)
            .HasColumnName("current_amount")
            .IsRequired();

        builder
            .HasOne(t => t.User)
            .WithMany(u => u.Targets);
    }
}