namespace SurveyBasket.Abstractions.Const;

public static class RegexPatterns
{
    public static string PasswordPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
    public static string PhoneNumberPattern = "^\\+?[1-9][0-9]{7,14}$";
}
