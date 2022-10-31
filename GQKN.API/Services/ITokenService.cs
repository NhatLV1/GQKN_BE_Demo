namespace PVI.GQKN.API.Services;

public interface ITokenService
{
    string BuildToken(string key, string issuer, ApplicationUser user);
    
    bool IsTokenValid(string key, string issuer, string token);
}
