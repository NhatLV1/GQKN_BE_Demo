namespace PVI.GQKN.API.Application.Commands.UserCommands;

public class LockUserRequest: IRequest<bool>
{
    public string UserId { get; set; }
}
