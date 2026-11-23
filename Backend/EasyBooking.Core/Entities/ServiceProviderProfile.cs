using EasyBooking.Core.Enums;

namespace EasyBooking.Core.Entities;

public class ServiceProviderProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? Address { get; set; }
    public ProviderApprovalStatus Status { get; set; } = ProviderApprovalStatus.Pending;
    public string? RejectionReason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReviewedAt { get; set; }
    public string? ReviewedByUserId { get; set; }

    // Navigation property  
    public AppUser? User { get; set; }
}