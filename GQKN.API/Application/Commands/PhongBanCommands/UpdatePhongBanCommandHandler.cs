using PVI.GQKN.Domain.Events;
using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.API.Application.Commands.DonViCommands;

public class UpdatePhongBanCommandHandler : IRequestHandler<UpdatePhongBanCommand, PhongBan>
{
    private readonly IPhongBanRepository _donviRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdatePhongBanCommandHandler> _logger;

    public UpdatePhongBanCommandHandler(
        IPhongBanRepository repository, 
        IMapper mapper, 
        ILogger<UpdatePhongBanCommandHandler> logger)
    {
        this._donviRepository = repository;
        this._mapper = mapper;
        this._logger = logger;
    }

    public async Task<PhongBan> Handle(UpdatePhongBanCommand request, CancellationToken cancellationToken)
    {
        var dv = await this._donviRepository.GetByGuidAsync(request._Id);

        if (dv != null)
        {
            this._mapper.Map(request, dv);
            
            this._donviRepository.Update(dv);
            dv.AddDomainEvent(new UpdateDonViDomainEvent(dv));

            await this._donviRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return dv;
        }

        return null;
    }
}
