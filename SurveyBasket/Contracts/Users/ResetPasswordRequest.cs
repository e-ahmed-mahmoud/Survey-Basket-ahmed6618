namespace SurveyBasket.Services.UserServices;

public record ResetPasswordRequest(string Email, string Code, string Password);
