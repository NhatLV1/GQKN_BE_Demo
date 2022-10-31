namespace PVI.GQKN.API.Services.Auth;

public abstract class ResourceAuthorizationHandler<T>
            : AuthorizationHandler<OperationAuthorizationRequirement, T>
{
    UserManager<ApplicationUser> _userManager;

    public ResourceAuthorizationHandler(UserManager<ApplicationUser>
        userManager)
    {
        _userManager = userManager;
    }

    public abstract Claim GetClaim(AuthorizationHandlerContext context,
                               OperationAuthorizationRequirement requirement,
                               T resource);

    protected override Task
        HandleRequirementAsync(AuthorizationHandlerContext context,
                               OperationAuthorizationRequirement requirement,
                               T resource)
    {
        if (context.User == null || resource == null)
        {
            return Task.CompletedTask;
        }

        Claim claim = GetClaim(context, requirement, resource);

        if (claim != null)
        {
            var claimValue = claim.Value;
            if (!string.IsNullOrEmpty(claimValue) && claimValue.Contains(requirement.Name))
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }

}
