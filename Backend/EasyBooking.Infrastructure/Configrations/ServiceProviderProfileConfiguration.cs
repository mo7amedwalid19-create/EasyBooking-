using EasyBooking.Core.Entities;
using EasyBooking.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBooking.Infrastructure.Persistence.Configurations;

public class ServiceProviderProfileConfiguration : IEntityTypeConfiguration<ServiceProviderProfile>
{
    public void Configure(EntityTypeBuilder<ServiceProviderProfile> builder)
    {
        builder.ToTable("ServiceProviderProfiles");

        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.UserId)
               .IsUnique();

        builder.Property(p => p.BusinessName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(p => p.Specialty)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Bio)
               .HasMaxLength(1000);

        builder.Property(p => p.Address)
               .HasMaxLength(300);

        builder.Property(p => p.RejectionReason)
               .HasMaxLength(500);

        builder.Property(p => p.Status)
               .HasConversion<string>()
               .HasMaxLength(30)
               .HasDefaultValue(ProviderApprovalStatus.Pending);

        builder.HasOne(p => p.User)
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
