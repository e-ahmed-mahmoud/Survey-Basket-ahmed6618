using SurveyBasket.Abstractions.Const;

namespace SurveyBasket.Contracts.Users;

public class RegisterRequestValidation : AbstractValidator<RegisterRequest>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterRequestValidation(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        // .MustAsync(async (email, token) =>
        // {
        //     var res = await UniqueEmailCheck(email, token);
        //     return !res;
        // }).WithMessage("Email is invalid");
        RuleFor(x => x.FirstName).NotEmpty().Length(2, 100);
        RuleFor(x => x.LastName).NotEmpty().Length(2, 100);
        RuleFor(x => x.Password).NotEmpty().Matches(RegexPatterns.PasswordPattern);
        RuleFor(x => x.PhoneNumber).NotEmpty().Matches(RegexPatterns.PhoneNumberPattern);

    }

    private async Task<bool> UniqueEmailCheck(string email, CancellationToken token) =>
       await _userManager.Users.AnyAsync(u => u.Email == email, token);
}
