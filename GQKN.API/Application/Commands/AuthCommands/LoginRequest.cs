
namespace PVI.GQKN.API.Application.Commands.AuthCommands;

public class LoginRequest: IRequest<LoginResult>
{
    [Required]
    public string Username { get; private set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; private set; }

    public LoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
