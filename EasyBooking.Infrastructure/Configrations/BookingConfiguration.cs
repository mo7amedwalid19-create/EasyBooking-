using EasyBooking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBooking.Infrastructure.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.Property(b => b.Notes)
            .HasMaxLength(500);

        // Restrict deletes to prevent SQL Server multiple cascade path errors
        builder.HasOne(b => b.Customer)
            .WithMany(u => u.CustomerBookings)
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.ProviderProfile)
            .WithMany(p => p.ProviderBookings)
            .HasForeignKey(b => b.ProviderProfileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}