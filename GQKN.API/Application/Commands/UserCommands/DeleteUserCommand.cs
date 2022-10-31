namespace PVI.GQKN.API.Application.Commands.UserCommands;

public class DeleteUserCommand : IRequest<bool>
{
    public string Id { get; set; }
}
