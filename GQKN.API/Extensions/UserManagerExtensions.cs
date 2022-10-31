public static class UserManagerExtensions
{
    public static async Task EnsureClaimAsync(this UserManager<ApplicationUser> userManager,
        ApplicationUser user, Claim claim)
    {
        await userManager.AddClaimAsync(user, claim);
    }

    public static async Task<string> EnsureUser(this UserManager<ApplicationUser> userManager,
         string username, string pwd)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null)
        {
            user = new ApplicationUser { UserName = username, Email = username, AccountType = AccountType.GQKN };
            var result = await userManager.CreateAsync(user, pwd);
        }

        return user.Id;
    }

    public static async Task<IdentityResult> EnsureUserRole(this UserManager<ApplicationUser> userManager,
                                                               string uid, string roleName)
    {
        IdentityResult IR = null;

        var user = await userManager.FindByIdAsync(uid.ToString());
        if (user != null)
        {
            IR = await userManager.AddToRoleAsync(user, roleName);
        }

        return IR;
    }

 
}