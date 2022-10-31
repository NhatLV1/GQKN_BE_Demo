namespace PVI.GQKN.API.Application.Commands.ThuMucCommands;

public class CreateThuMucCommandHandler : IRequestHandler<CreateThuMucCommand, ThuMuc>
{
    // DI: dependency injection
    private readonly IThuMucRepositoty _thuMucRepositoty;
    private readonly IMapper _mapper;

    public CreateThuMucCommandHandler(IThuMucRepositoty thuMucRepositoty, IMapper mapper)
    {
        this._thuMucRepositoty = thuMucRepositoty;
        this._mapper = mapper;
    }

    public async Task<ThuMuc> Handle(CreateThuMucCommand request, CancellationToken cancellationToken)
    {
        var dv = _mapper.Map<ThuMuc>(request);

        this._thuMucRepositoty.Insert(dv);

        await this._thuMucRepositoty.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return dv;
    }
}
