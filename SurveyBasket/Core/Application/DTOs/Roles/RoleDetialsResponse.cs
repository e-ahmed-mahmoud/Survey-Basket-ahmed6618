namespace SurveyBasket.Contracts.Roles;

public record RoleDetialsResponse(string Id, string Name, bool IsDeleted, IEnumerable<string> RoleClaims);

