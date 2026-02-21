namespace SurveyBasket.Services.UserServices;

public record ForgetPasswordRequest(string Email);


public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordRequest>
{
    public ForgetPasswordValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}