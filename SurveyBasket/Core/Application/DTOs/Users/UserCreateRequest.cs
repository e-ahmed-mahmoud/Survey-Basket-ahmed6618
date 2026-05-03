namespace SurveyBasket.Contracts.Users;

public record UserCreateRequest(string FirstName, string LastName, string Password, string PhoneNumber,
    string Email, IList<string> Roles);
