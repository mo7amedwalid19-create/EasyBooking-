using System.ComponentModel.DataAnnotations;

namespace EasyBooking.Services.DTOs.Providers;

public class CreateProviderProfileDto
{
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string BusinessName { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Specialty { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Bio { get; set; }

    [StringLength(300)]
    public string? Address { get; set; }
}