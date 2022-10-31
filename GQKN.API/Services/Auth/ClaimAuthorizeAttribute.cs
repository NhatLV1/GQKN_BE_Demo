namespace PVI.GQKN.API.Services.Auth;

public class ClaimAuthorizeAttribute : AuthorizeAttribute
{
    const string POLICY_PREFIX = "resource";

    public string Resource { get; set; }

    public ulong Claims { get; set; }

    public ClaimAuthorizeAttribute(string resource, ulong claims)
    {
        Resource = resource;
        Claims = claims;
        Policy = $"{POLICY_PREFIX}_{resource}_{claims}";
    }
}
