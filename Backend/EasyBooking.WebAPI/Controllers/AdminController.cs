using EasyBooking.Core.Enums;
using EasyBooking.Services.DTOs.Providers;
using EasyBooking.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyBooking.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = nameof(UserRole.SuperAdmin))]
public class AdminController : ControllerBase
{
    private readonly IProviderService _providerService;

    public AdminController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpGet("dashboard-check")]
    public IActionResult CheckAdminAccess()
    {
        return Ok(new
        {
            success = true,
            message = "Welcome SuperAdmin. RBAC is working properly."
        });
    }
    [HttpGet("providers/pending")]
    public async Task<IActionResult> GetPendingProviders(CancellationToken cancellationToken)
    {
        var profiles = await _providerService.GetPendingProfilesAsync(cancellationToken);
        return Ok(new { success = true, data = profiles });
    }

    
    [HttpPut("providers/{id:guid}/review")]
    public async Task<IActionResult> ReviewProvider(
        [FromRoute] Guid id,
        [FromBody] ReviewProviderDto dto,
        CancellationToken cancellationToken)
    {
        var adminUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(adminUserId))
        {
            return Unauthorized();
        }

        var result = await _providerService.ReviewProfileAsync(id, adminUserId, dto, cancellationToken);
        if (!result.Success)
        {
            return BadRequest(new { success = false, message = result.Message });
        }

        return Ok(new { success = true, message = result.Message, data = result.Data });
    }
}
