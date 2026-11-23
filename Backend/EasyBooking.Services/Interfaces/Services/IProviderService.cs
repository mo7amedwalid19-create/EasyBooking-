using EasyBooking.Services.DTOs.Providers;
using EasyBooking.Services.DTOs.Providers;
namespace EasyBooking.Services.Interfaces.Services
{
    public interface IProviderService
    {
        Task<(bool Success, string Message, ProviderProfileResponseDto? Data)> CreateProfileAsync(
            string userId,
            CreateProviderProfileDto dto,
            CancellationToken cancellationToken = default);

        Task<(bool Success, string Message, ProviderProfileResponseDto? Data)> GetMyProfileAsync(
            string userId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ProviderProfileResponseDto>> GetPendingProfilesAsync(
            CancellationToken cancellationToken = default);

        Task<(bool Success, string Message, ProviderProfileResponseDto? Data)> ReviewProfileAsync(
            Guid profileId,
            string reviewerUserId,
            ReviewProviderDto dto,
            CancellationToken cancellationToken = default);
    }
}
