
namespace PVI.GQKN.API.Application.Commands.VaiTroCommands;

/// <summary>
/// Yêu cầu tạo vai trò người dùng
/// </summary>
public class CreateVaiTroRequest: IRequest<ApplicationRole>
{
    /// <summary>
    /// Tên vai trò
    /// </summary>
    [Required]
    public string TenVaiTro { get;  set; }

    /// <summary>
    /// Mã phòng ban (optional)
    /// </summary>
    [DefaultValue(null)]
    public int? PhongBanId { get; private set; }

    /// <summary>
    /// Mã đơn vị (optional)
    /// </summary>
    [DefaultValue(null)]
    public int? DonViId { get; private set; }

    /// <summary>
    /// Danh sách mã các quyền
    /// </summary>
    public List<string> QuyenIds { get; private set; }


    public CreateVaiTroRequest(string tenVaiTro, int? phongBanId, int? donViId, List<string> quyenIds)
    {
        TenVaiTro = tenVaiTro;
        PhongBanId = phongBanId;
        QuyenIds = quyenIds;
        DonViId = donViId;
    }
}


public class CreateVaiTroRequestHandler : IRequestHandler<CreateVaiTroRequest, ApplicationRole>
{
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly ILogger<CreateVaiTroRequest> logger;
    private readonly IAuthService authService;
    private readonly GQKNDbContext context;
    private readonly IMapper mapper;

    public CreateVaiTroRequestHandler(
        RoleManager<ApplicationRole> roleManager,
        ILogger<CreateVaiTroRequest> logger, 
        IAuthService authService,
        GQKNDbContext context,
        IMapper mapper)
    {
        this.roleManager = roleManager;
        this.logger = logger;
        this.authService = authService;
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApplicationRole> Handle(CreateVaiTroRequest request, CancellationToken cancellationToken)
    {
        var role = mapper.Map<ApplicationRole>(request);
        
        var result = await roleManager.CreateAsync(role);

        if (request.QuyenIds != null)
        {
            var claims = request.QuyenIds.ToClaims();
            await roleManager.UpdateRoleClaims(role, claims);
        }

        if (result.Succeeded)
        {
            return role;
        }

        throw new GQKNDomainException(ErrorsDef.DB_UPDATE, result.ToString());

    }
}

