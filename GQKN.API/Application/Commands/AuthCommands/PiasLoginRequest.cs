
namespace PVI.GQKN.API.Application.Commands.AuthCommands;

public class PiasLoginRequest: IRequest<LoginResult>
{
    [Required]
    public string Username { get; private set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; private set; }

    public PiasLoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
