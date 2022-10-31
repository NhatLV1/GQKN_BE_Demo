namespace PVI.GQKN.API.Application.Commands.ThuMucCommands;

public class DeleteThuMucCommand : IRequest<bool>
{
    public string Id { get; set; }
}


