
namespace PVI.GQKN.API.Application.Commands.AuthCommands;

public class GQKNLoginRequest : IRequest<LoginResult>
{
    [Required]
    public string Username { get; private set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; private set; }

    public GQKNLoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
