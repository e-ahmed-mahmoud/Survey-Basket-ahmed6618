namespace SurveyBasket.Contracts.Users;

public record RegisterRequest(string Email, string FirstName, string LastName, string PhoneNumber, string Password);
