using Microsoft.CodeAnalysis.Operations;

namespace PVI.GQKN.API.Application.Commands.DonViCommands;

public class CreateDonViRequest: IRequest<DonVi>
{
    /// <summary>
    /// Mã đơn vị
    /// </summary>
    [MaxLength(10)]
    [Required]
    public string MaDonVi { get; set; }

    /// <summary>
    /// Tên đơn vị
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string TenDonVi { get; set; }
    
    /// <summary>
    /// Mã tỉnh
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string MaTinh { get; set; }

    /// <summary>
    /// Danh sách các scope (VD: ADMIN, KBTT, BCTT, ..)
    /// </summary>
    public List<string> Scopes { get; set; }
}

public class CreateDonViRequestHandler : IRequestHandler<CreateDonViRequest, DonVi>
{
    private readonly IDonViRepository donViRepository;
    private readonly ILogger<CreateDonViRequestHandler> logger;
    private readonly IMapper mapper;

    public CreateDonViRequestHandler(
        IDonViRepository donViRepository,
        ILogger<CreateDonViRequestHandler> logger,
        IMapper mapper)
    {
        this.donViRepository = donViRepository;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<DonVi> Handle(CreateDonViRequest request, CancellationToken cancellationToken)
    {
        var donVi = this.mapper.Map<DonVi>(request);
        this.donViRepository.Insert(donVi);

        await this.donViRepository.UnitOfWork.SaveEntitiesAsync();

        return donVi;

    }
}
