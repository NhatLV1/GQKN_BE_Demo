namespace PVI.GQKN.API.Application.Commands.UserCommands
{
    public class UnlockUserRequest: IRequest<bool>
    {
        public string UserId { get;  set; }
    }
}
