using EasyBooking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBooking.Infrastructure.Data.Configurations;

public class ProviderProfileConfiguration : IEntityTypeConfiguration<ProviderProfile>
{
    public void Configure(EntityTypeBuilder<ProviderProfile> builder)
    {
        builder.Property(p => p.Specialty)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Bio)
            .HasMaxLength(1000);

        builder.Property(p => p.City)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Region)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.HourlyRate)
            .HasColumnType("decimal(18,2)");

        // Composite index for fast location + category filtering
        builder.HasIndex(p => new { p.IsApproved, p.Category, p.City, p.Region });
    }
}