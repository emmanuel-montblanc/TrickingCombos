using Microsoft.AspNetCore.Identity;
using TrickingCombos.API.DTO;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Extensions;

public static class ApplicationUserExtension
{
    public static async Task<UserDTO> ToDto(this ApplicationUser user, UserManager<ApplicationUser> userManager)
    {
        var roles = await userManager.GetRolesAsync(user);
        string role = roles.FirstOrDefault()?.ToString() ?? "User";

        return new UserDTO
        {
            Id = new Guid(user.Id),
            Username = user.UserName,
            Email = user.Email,
            Role = role
        };
    }
}
