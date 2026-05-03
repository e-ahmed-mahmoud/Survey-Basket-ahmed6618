using SurveyBasket.Abstractions.Const;

namespace SurveyBasket.Services.UserServices;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(p => p.CurrentPassword).NotEmpty();
        RuleFor(p => p.NewPassword).NotEmpty()
            .Matches(RegexPatterns.PasswordPattern)
            .NotEqual(x => x.CurrentPassword);
    }
}