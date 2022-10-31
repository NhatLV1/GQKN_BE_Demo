using PVI.GQKN.Domain.Events;

namespace PVI.GQKN.API.Application.Commands.ThuMucCommands;

public class UpdateThuMucCommandHandler : IRequestHandler<UpdateThuMucCommand, ThuMuc>
{
    private readonly IThuMucRepositoty _thuMucRepositoty;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateThuMucCommandHandler> _logger;

    public UpdateThuMucCommandHandler(
        IThuMucRepositoty repository,
        IMapper mapper,
        ILogger<UpdateThuMucCommandHandler> logger)
    {
        _thuMucRepositoty = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ThuMuc> Handle(UpdateThuMucCommand request, CancellationToken cancellationToken)
    {
        var dv = await _thuMucRepositoty.GetByGuidAsync(request._Id);

        if (dv != null)
        {
            _mapper.Map(request, dv);

            _thuMucRepositoty.Update(dv);
            await _thuMucRepositoty.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return dv;
        }

        return null;
    }
}

