namespace PVI.GQKN.API.Application.Dtos;

public record LoginResultDto(string token, string message);

public class LoginResult
{
    public bool Successed { get; set; }
    public bool IsLockedOut { get; set; }
    public bool RequiresTwoFactor { get; set; }
    public bool IsNotAllowed { get; set; }
    public string  Token { get; set; }
    public string Message { get; internal set; }
    public string Status { get; internal set; }
}
