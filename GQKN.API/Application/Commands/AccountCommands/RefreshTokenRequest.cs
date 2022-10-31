namespace PVI.GQKN.API.Application.Commands.AccountCommands;

/// <summary>
/// Yêu cầu cấp GQKN token cho user <para>Username</para>
/// </summary>
public class RefreshTokenRequest: IRequest<RefreshTokenResult>
{
    /// <summary>
    /// Username liên kết token
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// Pias Token
    /// </summary>
    public string Token { get; set; }
    /// <summary>
    /// API Key
    /// </summary>
    public string Key { get; set; }
    /// <summary>
    /// Chữ kí. Tham khảo tài liệu.
    /// </summary>
    public string Sign { get; set; }
}
