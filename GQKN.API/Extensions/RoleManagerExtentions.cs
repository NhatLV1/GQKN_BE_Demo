using Microsoft.AspNetCore.Identity;

public static class RoleManagerExtentions
{
    public static async Task<ApplicationRole> EnsureRole(this RoleManager<ApplicationRole> roleManager,
           string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            role = new ApplicationRole(roleName);
            //role.Module = module;
            await roleManager.CreateAsync(role);
            role = await roleManager.FindByNameAsync(roleName);
        }

        return role;
    }

    public static async Task<string> EnsureClaim(this RoleManager<ApplicationRole> roleManager,
           GQKNDbContext context,
           string roleName, List<Claim> claims)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role != null)
        {
            foreach (var c in claims)
            {
                var claim = context.RoleClaims.FirstOrDefault(r =>
                    r.ClaimType == c.Type &&
                    r.RoleId == role.Id);

                if (claim == null)
                {
                    await roleManager.AddClaimAsync(role, c);
                }
                else
                {

                }
            }
        }

        return null;
    }

    public static async Task<string> EnsureRoleClaim(this RoleManager<ApplicationRole> roleManager,
        GQKNDbContext ctx,
            string roleName, List<Claim> claims)
    {

        // Ensure role exist
        var role = await EnsureRole(roleManager, roleName);

        // Ensure role claim exist
        await EnsureClaim(roleManager, ctx, roleName, claims);

        return null;
    }


    public static async Task UpdateRoleClaims(
        this RoleManager<ApplicationRole> roleManager,
        ApplicationRole role,
        IEnumerable<Claim> claims)
    {
        var roleClaims = await roleManager.GetClaimsAsync(role);

        foreach (var claim in claims)
        {
            var c = roleClaims.FirstOrDefault(e => e.Type == claim.Type);
            if (c == null)
            {
                await roleManager.AddClaimAsync(role, claim);
            }
            else
            {
                if (c != null && c.Value != claim.Value)
                {
                    // delete
                    await roleManager.RemoveClaimAsync(role, c);
                    await roleManager.AddClaimAsync(role, claim);
                }
            }

        }
    }

    public static async Task<List<string>> GetPermissionIdsAsync(
        this RoleManager<ApplicationRole> roleManager,
        ApplicationRole role, 
        IEnumerable<AclOperation> acls)
    {
        var claims = await roleManager.GetClaimsAsync(role);
        var ops = claims.ToOps(acls);
        var permisions = ops.Select(op => op.Key).ToList();

        return permisions;
    }

    public static async Task<string> UpdateUserRoles(
        this RoleManager<ApplicationRole> roleManager,
         UserManager<ApplicationUser> userManager,
         string[] selectedIds, ApplicationUser user)
    {
        if(selectedIds == null) 
            selectedIds = new string[0];

        var roles = await roleManager.Roles.ToArrayAsync();
        var allRoles = roles.Select(r => r.Name).ToArray();
        var selectedRoles = roles.Where(e => selectedIds.Contains(e.Id))
            .Select(e => e.Name).ToArray();

        var userRoles = await userManager.GetRolesAsync(user);

        foreach (var role in allRoles)
        {
            if (selectedRoles.Contains(role))
            {
                if (!userRoles.Contains(role)) // add
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
            else
            {
                if (userRoles.Contains(role))
                {
                    await userManager.RemoveFromRoleAsync(user, role);
                }
            }
        }

        return null;

    }
}