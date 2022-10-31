namespace PVI.GQKN.API.Application.Queries;

public class QueryKBTTRequestHandler :
    IRequestHandler<QueryKBTTRequest, PagedList<KhaiBaoTonThat>>
{
    private readonly IMapper mapper;
    private readonly IKhaiBaoTonThatRepository khaiBaoTonThatRepo;
    private readonly ILogger<QueryKBTTRequestHandler> logger;

    public QueryKBTTRequestHandler(
        IIdentityService identityService,
        IMapper mapper,
        IKhaiBaoTonThatRepository khaibaoTonThatRepo,
        ILogger<QueryKBTTRequestHandler> logger)
    {
        this.mapper = mapper;
        this.khaiBaoTonThatRepo = khaibaoTonThatRepo;
        this.logger = logger;
    }

    public async Task<PagedList<KhaiBaoTonThat>> Handle(
        QueryKBTTRequest request,
        CancellationToken cancellationToken)
    {
        return await this.khaiBaoTonThatRepo.GetPage(request, "DonViCapDon,GQKN");

    }
}
