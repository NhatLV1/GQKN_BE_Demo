using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.API.Application.Commands.DonViCommands;

public class DeletePhongBanCommandHandler: IRequestHandler<DeletePhongBanCommand, bool>
{
    private readonly IPhongBanRepository _donviRepository;

    public DeletePhongBanCommandHandler(IPhongBanRepository repository)
    {
        this._donviRepository = repository;
    }

    public async Task<bool> Handle(DeletePhongBanCommand request, CancellationToken cancellationToken)
    {
        if (await _donviRepository.DeleteByGuidAsync(request.Id))
        {
            await _donviRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}
