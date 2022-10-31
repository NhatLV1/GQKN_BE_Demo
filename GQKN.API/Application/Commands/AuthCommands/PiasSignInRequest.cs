namespace PVI.GQKN.API.Application.Commands.AuthCommands;

public class PiasSignInRequest: IRequest<PiasSignInResult>
{
    public string Username { get; set; }

    public string Password { get; set; }

    public PiasSignInRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
