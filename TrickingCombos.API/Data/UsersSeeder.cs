using Microsoft.AspNetCore.Identity;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Data;

public class UsersSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Create roles
        string[] roles = ["Admin", "User"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        var username = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
        var email = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        var password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new Exception("Missing admin credentials in environment.");

        var adminUser = await userManager.FindByEmailAsync(email);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = username,
                Email = email,
            };
            var result = await userManager.CreateAsync(adminUser, password);

            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
