using SurveyBasket.Services.UserServices;

namespace SurveyBasket.Core.Application.Interfaces;

public interface IUserService
{
    Task<Result<UserProfile>> GetAccountInfoAsync(string Id);

    Task<Result> UpdateUserAccountAsync(UpdateAccountRequest request, string userId);
    Task<Result> DeActiveUserAccountAsync(string userId);

    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);

    Task<IEnumerable<UserResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Result<UserResponse>> GetByIdAsync(string id);
    Task<Result<UserResponse>> AddAsync(UserCreateRequest request, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(string id, UserUpdateRequest request, CancellationToken cancellationToken = default);

}
