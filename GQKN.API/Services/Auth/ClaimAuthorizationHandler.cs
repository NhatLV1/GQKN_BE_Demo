namespace PVI.GQKN.API.Services.Auth;

public class ClaimAuthorizationHandler : AuthorizationHandler<ClaimRequirement>
{
    
    private readonly ILogger<ClaimAuthorizationHandler> _logger;
    //private readonly RoleManager<ApplicationRole> roleManager;

    public ClaimAuthorizationHandler(ILogger<ClaimAuthorizationHandler> logger)
    {
        //
        _logger = logger;
        //this.roleManager = roleManager;
    }

    // Check whether a given MinimumAgeRequirement is satisfied or not for a particular context
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        ClaimRequirement requirement)
    {

#if DEBUG
        context.Succeed(requirement);
#endif
        if (context.User == null)
        {
            return Task.CompletedTask;
        }

        if (context.User.IsSuperAdmin())
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var claim = 
            context.User.Claims.FirstOrDefault(c => c.Type == requirement.ClaimType);
        
        if (claim != null)
        {
            if (ulong.TryParse(claim.Value, out ulong claimValue))
            {
                var mask = claimValue & requirement.ClaimValue;
                if (mask != 0)
                {
                    context.Succeed(requirement);
                }
            }
        }
       
        return Task.CompletedTask;
    }
}
