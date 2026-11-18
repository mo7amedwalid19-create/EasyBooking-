using EasyBooking.Core.Enums;
 
namespace EasyBooking.Core.Entities
{
    public class ProviderProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;

        public ProviderCategory Category { get; set; }
        public string Specialty { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; }

        // Geographic Location (Structured for Task 1.4 Onboarding & Task 2.1 Search)
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

        public bool IsApproved { get; set; } = false;

        // Navigation Properties
        public ICollection<Booking> ProviderBookings { get; set; } = new List<Booking>();
    }
}
