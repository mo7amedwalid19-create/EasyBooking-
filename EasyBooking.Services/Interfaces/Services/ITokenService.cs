using EasyBooking.Core.Entities;

namespace EasyBooking.Services.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user, IEnumerable<string> roles);
    }
}
