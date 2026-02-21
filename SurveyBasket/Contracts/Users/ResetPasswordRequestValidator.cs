using Microsoft.Identity.Client;
using SurveyBasket.Abstractions.Const;

namespace SurveyBasket.Services.UserServices;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().Matches(RegexPatterns.PasswordPattern);
    }
}
