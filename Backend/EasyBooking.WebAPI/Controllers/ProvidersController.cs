
using EasyBooking.Services.DTOs.Providers;
using EasyBooking.Core.Enums;
using EasyBooking.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyBooking.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = nameof(UserRole.Provider))]
public class ProvidersController : ControllerBase
{
    private readonly IProviderService _providerService;

    public ProvidersController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpPost("profile")]
    public async Task<IActionResult> CreateProfile(
        [FromBody] CreateProviderProfileDto dto,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var result = await _providerService.CreateProfileAsync(userId, dto, cancellationToken);
        if (!result.Success)
        {
            return BadRequest(new { success = false, message = result.Message });
        }

        return CreatedAtAction(nameof(GetMyProfile), new { success = true, message = result.Message, data = result.Data });
    }

    [HttpGet("profile/me")]
    public async Task<IActionResult> GetMyProfile(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var result = await _providerService.GetMyProfileAsync(userId, cancellationToken);
        if (!result.Success)
        {
            return NotFound(new { success = false, message = result.Message });
        }

        return Ok(new { success = true, message = result.Message, data = result.Data });
    }
}