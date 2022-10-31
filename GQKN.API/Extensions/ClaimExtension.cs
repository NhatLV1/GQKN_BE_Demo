using Duende.IdentityServer.Models;

namespace PVI.GQKN.API.Extensions;

public static class ClaimExtension
{
    public static bool IsSuperAdmin(this ClaimsPrincipal user)
    {
        if (user == null)
            return false;

        var claim = user.Claims.FirstOrDefault(e => e.Type == IAuthService.SUPER_ADMIN_CLAIM_NAME);

        if (claim == null) 
            return false;

        return true;
    }

    public static IEnumerable<AclOperation> ToOps(this Claim claim, IEnumerable<AclOperation> acls)
    {
        List<AclOperation> ops = new List<AclOperation>();
        if (ulong.TryParse(claim.Value, out var val))
        {
            foreach (var op in acls)
            {
                var mask = val & op.Id;
                if (op.Scope.Equals(claim.Type, StringComparison.InvariantCultureIgnoreCase)
                    && mask != 0)
                {
                    ops.Add(op);
                }
            }
        }
        return ops;
    }

   
}
