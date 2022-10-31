namespace PVI.GQKN.API.Services.Auth;

public class ClaimRequirement : IAuthorizationRequirement
{
    public string ClaimType { get; set; }

    public ulong ClaimValue { get; set; }

    public ClaimRequirement(string claimType, ulong claimValue)
    {
        ClaimType = claimType;
        ClaimValue = claimValue;
    }
}
