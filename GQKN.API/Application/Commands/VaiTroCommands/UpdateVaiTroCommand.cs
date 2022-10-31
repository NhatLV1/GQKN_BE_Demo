using PVI.GQKN.API.Services.Auth;

namespace PVI.GQKN.API.Application.Commands.VaiTroCommands;

[DataContract]
public class UpdateVaiTroRequest: IRequest<ApplicationRole>
{
    internal string Id { get; set; }

    /// <summary>
    /// Tên vai trò
    /// </summary>
    [DataMember]
    [Required]
    public string TenVaiTro { get; set; }

    /// <summary>
    /// Mã phòng ban, mỗi vai trò thuộc về một phòng ban
    /// </summary>
    [DefaultValue(null)]
    [DataMember]
    public int? PhongBanId { get; set; }

    /// <summary>
    /// Mã đơn vị (optional)
    /// </summary>
    [DefaultValue(null)]
    public int? DonViId { get; private set; }

    [DefaultValue(null)]
    [DataMember(EmitDefaultValue = true)]
    /// <summary>
    /// Danh sách mã quyền
    /// </summary>
    public List<string> QuyenIds { get; set; }
}

public class UpdateVaiTroRequestHandler : IRequestHandler<UpdateVaiTroRequest, ApplicationRole>
{
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly ILogger<UpdateVaiTroRequestHandler> logger;
    private readonly IAuthService authService;
    private readonly IMapper mapper;

    public UpdateVaiTroRequestHandler(
        RoleManager<ApplicationRole> roleManager,
        ILogger<UpdateVaiTroRequestHandler> logger,
        IAuthService authService,
        IMapper mapper)
    {
        this.roleManager = roleManager;
        this.logger = logger;
        this.authService = authService;
        this.mapper = mapper;
    }

    public async Task<ApplicationRole> Handle(UpdateVaiTroRequest request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id);
        if (role != null)
        {
            role = mapper.Map(request, role);

            try
            {
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    var claims = request.QuyenIds.ToClaims();
                    await roleManager.UpdateRoleClaims(role, claims);
                    return role;
                }
                throw new GQKNDomainException(result.ToString());
            }
            catch (DbUpdateException ex)
            {
                throw new GQKNDomainException(ex.InnerException.ToString());
            }
        }

        return role;
    }
}


