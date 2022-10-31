using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.API.Application.Commands.DonViCommands;

public class CreatePhongBanCommandHandler : IRequestHandler<CreatePhongBanCommand, PhongBan>
{
    // DI: dependency injection
    private readonly IPhongBanRepository _donviRepository; 
    private readonly IMapper _mapper;

    public CreatePhongBanCommandHandler(IPhongBanRepository donViRepository, IMapper mapper)
    {
        this._donviRepository = donViRepository;
        this._mapper = mapper;
    }

    public async Task<PhongBan> Handle(CreatePhongBanCommand request, CancellationToken cancellationToken)
    {
        var dv = _mapper.Map<PhongBan>(request);

        _donviRepository.Insert(dv);

        await _donviRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return dv;
    }
}
