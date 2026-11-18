 

namespace EasyBooking.Services.DTOs.Auth
{
    

    public class AuthResultDto
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
