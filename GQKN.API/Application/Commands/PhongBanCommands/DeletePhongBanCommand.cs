namespace PVI.GQKN.API.Application.Commands.DonViCommands;

public class DeletePhongBanCommand: IRequest<bool>
{
    public string Id { get; set; }
}
