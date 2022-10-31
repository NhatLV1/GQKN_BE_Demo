//using System.IdentityModel.Tokens.Jwt;

//namespace PVI.GQKN.API.Services;


//public class TokenService : ITokenService
//{
//    private const double EXPIRY_DURATION_MINUTES = 30;
//    private readonly UserManager<ApplicationUser> _userManager;
//    private readonly RoleManager<ApplicationUser> _roleManager;

//    public TokenService(UserManager<ApplicationUser> userManager,
//        RoleManager<ApplicationUser> roleManager)
//    {
//        this._userManager = userManager;
//        this._roleManager = roleManager;
//    }

//    private async Task<List<Claim>> GetClaims(ApplicationUser user)
//    {
//        IdentityOptions _options = new IdentityOptions();

//        var claims = new List<Claim>()
//        {
//            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
//            new Claim("Id", user.Id),
//            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
//            new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
//            new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName)
//        };

//        var userClaims = await _userManager.GetClaimsAsync(user);
//        claims.AddRange(userClaims);
//        var userRoles = await _userManager.GetRolesAsync(user);
//        foreach (var userRole in userRoles)
//        {
//            claims.Add(new Claim(ClaimTypes.Role, userRole));
//            var role = await _roleManager.FindByNameAsync(userRole);
//            if (role != null)
//            {
//                var roleClaims = await _roleManager.GetClaimsAsync(role);
//                foreach (Claim roleClaim in roleClaims)
//                {
//                    claims.Add(roleClaim);
//                }
//            }
//        }
//        return claims;
//    }

//    public string BuildToken(string key, string issuer, ApplicationUser user)
//    {
//        var claims = new[] {
//            new Claim(ClaimTypes.Name, user.UserName),
//            new Claim(ClaimTypes.Role, user.Role),
//            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
//        };

//        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
//        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
//        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
//            expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);

//        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
//    }

//    //public string GenerateJSONWebToken(string key, string issuer, UserDTO user)
//    //{
//    //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
//    //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//    //    var token = new JwtSecurityToken(issuer, issuer,
//    //      null,
//    //      expires: DateTime.Now.AddMinutes(120),
//    //      signingCredentials: credentials);

//    //    return new JwtSecurityTokenHandler().WriteToken(token);
//    //}

//    public bool IsTokenValid(string key, string issuer, string token)
//    {
//        var mySecret = Encoding.UTF8.GetBytes(key);
//        var mySecurityKey = new SymmetricSecurityKey(mySecret);

//        var tokenHandler = new JwtSecurityTokenHandler();
//        try
//        {
//            tokenHandler.ValidateToken(token, new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                ValidateIssuer = true,
//                ValidateAudience = true,
//                ValidIssuer = issuer,
//                ValidAudience = issuer,
//                IssuerSigningKey = mySecurityKey,
//            }, out SecurityToken validatedToken);
//        }
//        catch
//        {
//            return false;
//        }
//        return true;
//    }
//}
