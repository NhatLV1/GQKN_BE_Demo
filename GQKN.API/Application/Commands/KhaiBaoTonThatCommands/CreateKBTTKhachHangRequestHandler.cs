namespace PVI.GQKN.API.Application.Commands.KhaiBaoTonThatCommands;

public class CreateKBTTKhachHangRequestHandler : 
    IRequestHandler<CreateKBTTKhachHangRequest, KhaiBaoTonThat>
{
    private readonly ILogger<CreateKBTTKhachHangRequest> logger;
    private readonly IMapper mapper;
    private readonly IKhaiBaoTonThatRepository khaiBaoTonThatRepository;
    private readonly IIdentityService identityService;

    public CreateKBTTKhachHangRequestHandler(
        IMapper mapper,
        IKhaiBaoTonThatRepository khaiBaoTonThatRepository,
        ILogger<CreateKBTTKhachHangRequest> logger,
        IIdentityService identityService)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.khaiBaoTonThatRepository = khaiBaoTonThatRepository;
        this.identityService = identityService;
    }

    public async Task<KhaiBaoTonThat> Handle(CreateKBTTKhachHangRequest request, CancellationToken cancellationToken)
    {
        var entity = this.mapper.Map<KhaiBaoTonThat>(request);

        try
        {
            //TODO: extra field: MaDinhDanh
            entity.NguoiTaoId = identityService.GetUserId();
            entity.MaDinhDanh = "FAKE";

            this.khaiBaoTonThatRepository.Insert(entity);

            await this.khaiBaoTonThatRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return entity;
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e.Message);
            throw new GQKNDomainException(ErrorsDef.DB_UPDATE, e.Message, e.InnerException);
        }
    }

}
