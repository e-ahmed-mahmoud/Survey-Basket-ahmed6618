namespace SurveyBasket.Authentication.Authorization;

public class PermissionsRequirement(string policy) : IAuthorizationRequirement
{
    public string Policy { get; } = policy;
}
