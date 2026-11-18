using EasyBooking.Core.Entities;
using EasyBooking.Services.DTOs.Auth;
using EasyBooking.Services.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace EasyBooking.Services.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<AuthResultDto> RegisterAsync(RegisterRequest request)
    {
        var role = request.Role.Trim();

        if (role != "Customer" && role != "Provider")
        {
            return new AuthResultDto
            {
                StatusCode = 400,
                Success = false,
                Message = "Invalid role."
            };
        }

        var existingUser = await _userManager.FindByEmailAsync(request.Email.Trim());
        if (existingUser != null)
        {
            return new AuthResultDto
            {
                StatusCode = 409,
                Success = false,
                Message = "Email already registered."
            };
        }

        var user = new AppUser
        {
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim(),
            UserName = request.Email.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return new AuthResultDto
            {
                StatusCode = 400,
                Success = false,
                Message = "Registration failed.",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        var roleResult = await _userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            return new AuthResultDto
            {
                StatusCode = 500,
                Success = false,
                Message = "Could not assign user role."
            };
        }

        return new AuthResultDto
        {
            StatusCode = 201,
            Success = true,
            Message = "Account created successfully.",
            Data = new
            {
                userId = user.Id,
                email = user.Email,
                role
            }
        };
    }

    public async Task<AuthResultDto> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email.Trim());
        if (user == null)
        {
            return new AuthResultDto
            {
                StatusCode = 401,
                Success = false,
                Message = "Invalid email or password."
            };
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(
            user, request.Password, lockoutOnFailure: true);

        if (signInResult.IsLockedOut)
        {
            return new AuthResultDto
            {
                StatusCode = 401,
                Success = false,
                Message = "Account is temporarily locked due to multiple failed login attempts."
            };
        }

        if (!signInResult.Succeeded)
        {
            return new AuthResultDto
            {
                StatusCode = 401,
                Success = false,
                Message = "Invalid email or password."
            };
        }

        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Count == 0)
        {
            return new AuthResultDto
            {
                StatusCode = 401,
                Success = false,
                Message = "User has no assigned role."
            };
        }

        var token = _tokenService.GenerateToken(user, roles);

        return new AuthResultDto
        {
            StatusCode = 200,
            Success = true,
            Message = "Login successful.",
            Data = new
            {
                token,
                role = roles[0],
                fullName = user.FullName
            }
        };
    }
}
