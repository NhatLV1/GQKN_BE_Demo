namespace PVI.GQKN.API.Application.Commands.ThuMucCommands;

public class DeleteThuMucCommandHandler : IRequestHandler<DeleteThuMucCommand, bool>
{
    private readonly IThuMucRepositoty _thuMucRepositoty;

    public DeleteThuMucCommandHandler(IThuMucRepositoty repository)
    {
        this._thuMucRepositoty = repository;
    }

    public async Task<bool> Handle(DeleteThuMucCommand request, CancellationToken cancellationToken)
    {
        if (await this._thuMucRepositoty.DeleteByGuidAsync(request.Id))
        {
            await this._thuMucRepositoty.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}

