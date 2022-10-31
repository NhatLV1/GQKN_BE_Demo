namespace PVI.GQKN.API.Application.Commands.UserCommands;

public class SetUserPasswordRequest : IRequest<bool>
{
    /// <summary>
    /// 
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Mật khẩu người dùng
    /// </summary>
    [Required]
    public string Password { get; set; }
}
