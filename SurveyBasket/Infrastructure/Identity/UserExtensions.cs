using System.Security.Claims;

namespace SurveyBasket.Extensions;

public static class UserExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user) => user.FindFirstValue(ClaimTypes.NameIdentifier);

    public async static Task<bool> CheckEmailAsync(UserManager<ApplicationUser> userManager, string email) => await userManager.FindByEmailAsync(email) is not null;

}
