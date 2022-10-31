namespace PVI.GQKN.API.Application.Queries;

[DataContract]
public class QueryPhongBanCommand : PagedListQueryParams,
    IRequest<PagedList<PhongBan>>
{
}

public class QueryPhongBanCommandHandler : IRequestHandler<QueryPhongBanCommand, GQKN.Infrastructure.Contracts.PagedList<PhongBan>>
{
    private readonly IPhongBanRepository _phongBanRepository;

    public QueryPhongBanCommandHandler(IPhongBanRepository donViRepository)
    {
        this._phongBanRepository = donViRepository;
    }

    public async Task<PagedList<PhongBan>> 
        Handle(QueryPhongBanCommand request, CancellationToken cancellationToken)
    {
        var page = await this._phongBanRepository.GetPage(request);
        return page;
    }
}

public class GetPhongBanByIdCommand: IRequest<PhongBan>
{
    public string Id { get; set; }
}

public class GetPhongBanByIdCommandHandler : IRequestHandler<GetPhongBanByIdCommand, PhongBan>
{
    private readonly ILogger<GetPhongBanByIdCommandHandler> logger;
    private readonly IPhongBanRepository phongBanRepository;

    public GetPhongBanByIdCommandHandler(ILogger<GetPhongBanByIdCommandHandler> logger,
        IPhongBanRepository phongBanRepository)
    {
        this.logger = logger;
        this.phongBanRepository = phongBanRepository;
    }
    public async Task<PhongBan> Handle(GetPhongBanByIdCommand request, CancellationToken cancellationToken)
    {
        return await phongBanRepository.GetByGuidAsync(request.Id);
    }
}
