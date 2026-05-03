namespace SurveyBasket.Contracts.Users;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(t => t.Token).NotEmpty();
        RuleFor(t => t.RefreshToken).NotEmpty();
    }
}
