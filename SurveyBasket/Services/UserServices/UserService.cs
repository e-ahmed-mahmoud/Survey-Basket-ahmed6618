using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using SurveyBasket.Extensions.Emails;

namespace SurveyBasket.Services.UserServices;

public class UserService(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<UserProfile>> GetAccountInfoAsync(string userId)
    {
        var userProfile = await _userManager.Users.Where(u => u.Id == userId).AsNoTracking().ProjectToType<UserProfile>().SingleAsync();
        return Result.Success(userProfile);
    }

    public async Task<Result> UpdateUserAccountAsync(UpdateAccountRequest request, string userId)
    {
        // var user = await _userManager.Users.Where(u => u.Id == userId).SingleAsync();
        // user = request.Adapt(user);
        // await _userManager.UpdateAsync(user);
        // using ExecutedUpdated will update data in single query in one connection without loading it into Memory
        // unlike selecting user into memory then updated in another query
        var result = await _userManager.Users.Where(u => u.Id == userId)
            .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(u => u.FirstName, request.FirstName)
                            .SetProperty(u => u.LastName, request.LastName));
        return Result.Success();
    }

    public async Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);
        if (result.Succeeded)
        {
            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }


}
