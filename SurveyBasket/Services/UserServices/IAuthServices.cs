namespace SurveyBasket.Services.UserServices;

public interface IAuthServices
{
    Task<Result<UserAuthResponse>> GetAuthTokenAsync(string email, string password, CancellationToken cancellationToken = default);

    Task<Result<UserAuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);

    Task<Result> RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken = default);
    Task<Result> ConfirmEmail(EmailConfirm request);
    Task<Result> ResendEmailConfirmationCode(ResendConfirmationEmailCodeRequest request);
    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);

    Task<Result> ForgetPasswordAsync(string userId, ForgetPasswordRequest request);

}
