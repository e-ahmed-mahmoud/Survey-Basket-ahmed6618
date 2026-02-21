namespace SurveyBasket.Services.UserServices;

public class ResendConfirmationEmailCodeRequestValidator : AbstractValidator<ResendConfirmationEmailCodeRequest>
{
    public ResendConfirmationEmailCodeRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
