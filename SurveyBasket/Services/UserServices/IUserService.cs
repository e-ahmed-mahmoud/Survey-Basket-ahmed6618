using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Modes.Gcm;

namespace SurveyBasket.Services.UserServices;

public interface IUserService
{
    Task<Result<UserProfile>> GetAccountInfoAsync(string Id);

    Task<Result> UpdateUserAccountAsync(UpdateAccountRequest request, string userId);

    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
}
