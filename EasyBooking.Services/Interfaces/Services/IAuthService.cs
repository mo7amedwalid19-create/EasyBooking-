using EasyBooking.Services.DTOs.Auth;

namespace EasyBooking.Services.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(RegisterRequest request);
        Task<AuthResultDto> LoginAsync(LoginRequest request);
    }

}
