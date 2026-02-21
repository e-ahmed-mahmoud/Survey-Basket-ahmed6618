namespace SurveyBasket.Services.UserServices;

public class EmailConfirmValidator : AbstractValidator<EmailConfirm>
{
    public EmailConfirmValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
    }
}
