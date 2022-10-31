namespace PVI.GQKN.API.Application.Commands.AccountCommands;

/// <summary>
/// Gửi email reset password
/// Return true nếu gửi email gửi thành công
/// Return false nếu gửi email không thành công.
/// </summary>
public class ResetPasswordRequest : IRequest<bool>
{
    public string Email { get; private set; }

	public ResetPasswordRequest(string email)
	{
		Email = email;
	}
}
