namespace EasyBooking.Services.DTOs.Providers;

public class ProviderProfileResponseDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? Address { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? RejectionReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
}
