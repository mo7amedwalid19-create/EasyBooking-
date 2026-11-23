using EasyBooking.Services.DTOs.Providers;
using EasyBooking.Core.Entities;
using EasyBooking.Core.Enums;
using EasyBooking.Services.DTOs.Providers;
using EasyBooking.Services.Interfaces.Repository;
using EasyBooking.Services.Interfaces.Services;
 

namespace EasyBooking.Services.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<ServiceProviderProfile> _profileRepository;

        public ProviderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _profileRepository = _unitOfWork.Repository<ServiceProviderProfile>();
        }

        public async Task<(bool Success, string Message, ProviderProfileResponseDto? Data)> CreateProfileAsync(
            string userId,
            CreateProviderProfileDto dto,
            CancellationToken cancellationToken = default)
        {
            var existingProfiles = await _profileRepository.FindAsync(p => p.UserId == userId);
            var alreadyExists = existingProfiles.Any();

            if (alreadyExists)
            {
                return (false, "A provider profile already exists for this account.", null);
            }
            var profile = new ServiceProviderProfile
            {
                UserId = userId,
                BusinessName = dto.BusinessName.Trim(),
                Specialty = dto.Specialty.Trim(),
                Bio = dto.Bio?.Trim(),
                Address = dto.Address?.Trim(),
                Status = ProviderApprovalStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _profileRepository.AddAsync(profile);
            await _unitOfWork.CompleteAsync();

            return (true, "Provider profile submitted and pending SuperAdmin approval.", MapToDto(profile));
        }

        public async Task<(bool Success, string Message, ProviderProfileResponseDto? Data)> GetMyProfileAsync(
            string userId,
            CancellationToken cancellationToken = default)
        {
            var profiles = await _profileRepository.FindAsync(p => p.UserId == userId);
            var profile = profiles.FirstOrDefault();

            if (profile is null)
            {
                return (false, "Provider profile not found.", null);
            }

            return (true, "Provider profile retrieved successfully.", MapToDto(profile));
        }

        public async Task<IEnumerable<ProviderProfileResponseDto>> GetPendingProfilesAsync(
            CancellationToken cancellationToken = default)
        {
            var pendingProfiles = await _profileRepository.FindAsync(
                p => p.Status == ProviderApprovalStatus.Pending);

            return pendingProfiles
                .OrderBy(p => p.CreatedAt)
                .Select(MapToDto);
        }

        public async Task<(bool Success, string Message, ProviderProfileResponseDto? Data)> ReviewProfileAsync(
            Guid profileId,
            string reviewerUserId,
            ReviewProviderDto dto,
            CancellationToken cancellationToken = default)
        {
            var profiles = await _profileRepository.FindAsync(p => p.Id == profileId);
            var profile = profiles.FirstOrDefault();

            if (profile is null)
            {
                return (false, "Provider profile not found.", null);
            }

            if (!dto.Approve && string.IsNullOrWhiteSpace(dto.RejectionReason))
            {
                return (false, "A rejection reason is required when rejecting a provider profile.", null);
            }

            profile.Status = dto.Approve
                ? ProviderApprovalStatus.Approved
                : ProviderApprovalStatus.Rejected;

            profile.RejectionReason = dto.Approve ? null : dto.RejectionReason!.Trim();
            profile.ReviewedAt = DateTime.UtcNow;
            profile.ReviewedByUserId = reviewerUserId;

            _profileRepository.Update(profile);
            await _unitOfWork.CompleteAsync();

            var message = dto.Approve
                ? "Provider profile approved successfully."
                : "Provider profile rejected.";

            return (true, message, MapToDto(profile));
        }
 
        private static ProviderProfileResponseDto MapToDto(ServiceProviderProfile profile) => new()
        {
            Id = profile.Id,
            UserId = profile.UserId,
            BusinessName = profile.BusinessName,
            Specialty = profile.Specialty,
            Bio = profile.Bio,
            Address = profile.Address,
            Status = profile.Status.ToString(),
            RejectionReason = profile.RejectionReason,
            CreatedAt = profile.CreatedAt,
            ReviewedAt = profile.ReviewedAt
        };
    }
    }
