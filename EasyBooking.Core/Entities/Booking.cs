using EasyBooking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBooking.Core.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        public string CustomerId { get; set; } = string.Empty;
        public AppUser Customer { get; set; } = null!;

        public int ProviderProfileId { get; set; }
        public ProviderProfile ProviderProfile { get; set; } = null!;
    }
}
