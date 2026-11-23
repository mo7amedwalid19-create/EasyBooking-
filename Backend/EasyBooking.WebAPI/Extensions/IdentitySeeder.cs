using EasyBooking.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
namespace EasyBooking.WebAPI.Extensions;


public static class IdentitySeeder
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        string[] roles = { "Customer", "Provider", "SuperAdmin" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        const string adminEmail = "admin@easybooking.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "System SuperAdmin",
                EmailConfirmed = true
            };

            // Use a strong password meeting your Identity password policy
            var result = await userManager.CreateAsync(newAdmin, "Admin@123456");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "SuperAdmin");
            }
        }
        }
}
