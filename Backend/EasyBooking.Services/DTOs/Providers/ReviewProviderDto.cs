
using System.ComponentModel.DataAnnotations;
 

namespace EasyBooking.Services.DTOs.Providers
{
    public class ReviewProviderDto
    {
        [Required]
        public bool Approve { get; set; }

        [StringLength(500)]
        public string? RejectionReason { get; set; }
    }

}
