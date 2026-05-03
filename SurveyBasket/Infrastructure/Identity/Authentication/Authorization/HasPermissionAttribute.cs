namespace SurveyBasket.Authentication.Authorization;
//pass policyName to Base class
public class HasPermissionAttribute(string policyName) : AuthorizeAttribute(policyName);
