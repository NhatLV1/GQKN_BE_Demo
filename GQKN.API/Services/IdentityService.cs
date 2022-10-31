using System.IdentityModel.Tokens.Jwt;

namespace PVI.GQKN.API.Services;

public class IdentityService : IIdentityService
{
    private IHttpContextAccessor _context;
    public IdentityService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public int? GetDonViId()
    {
        var idVal = _context.HttpContext.User.FindFirst(IIdentityService.DONVI_ID_CLAIM_NAME)?.Value;
        return idVal == null ? null : int.Parse(idVal);
    }

    public int? GetUserId()
    {
        var idVal =  _context.HttpContext.User.FindFirst(IIdentityService.USER_ID_CLAIM_NAME)?.Value;
        return idVal == null ? null : int.Parse(idVal);
    }

    public string GetUserIdentity()
    {
        return _context.HttpContext.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
    }

    public string GetUserName()
    {
        return _context.HttpContext.User?.Identity?.Name;
    }

    public bool IsSuperAdmin()
    {
        if (this._context.HttpContext.User == null) 
            return false;

        return this._context.HttpContext.User.IsSuperAdmin();
    }
}
