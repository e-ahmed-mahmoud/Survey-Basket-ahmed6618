namespace SurveyBasket.Services.UserServices;

public record ChangePasswordRequest(string CurrentPassword, string NewPassword);
