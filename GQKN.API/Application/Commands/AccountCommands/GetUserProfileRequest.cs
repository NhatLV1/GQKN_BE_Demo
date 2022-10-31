namespace PVI.GQKN.API.Application.Commands.AccountCommands;

public class GetUserProfileRequest: IRequest<UserProfile>
{
}

public class GetUserProfileRequestHandler : IRequestHandler<GetUserProfileRequest, UserProfile>
{
    private readonly IIdentityService identityService;
    private readonly IUserRepository userRepository;
    private readonly IAuthService authService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly IApplicationRoleRepository roleRepository;
    private readonly IMapper mapper;

    public GetUserProfileRequestHandler(
        IIdentityService identityService,
        IUserRepository userRepository,
        IAuthService authService,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IApplicationRoleRepository roleRepository,
        IMapper mapper)
    {
        this.identityService = identityService;
        this.userRepository = userRepository;
        this.authService = authService;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }

    public async Task<UserProfile> Handle(GetUserProfileRequest request, CancellationToken cancellationToken)
    {
        var username = identityService.GetUserName();

        var user = await userRepository.FirstOrDefaultAsync(e => e.UserName == username,
            "ChucDanh,PhongBan");

        var profile = mapper.Map<UserProfile>(user);

        var userRoles = await this.userManager.GetRolesAsync(user);
        var vaiTros = await this.roleRepository.FindRolesWithNames(userRoles);

        profile.VaiTro = mapper.Map<IEnumerable<VaiTroInfo>>(vaiTros);

        var acls = authService.GetACL();
        var permissionBag = new List<AclOperation>();
        foreach (var role in vaiTros)
        {
            var claims = await roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                permissionBag.AddRange(claim.ToOps(acls));
            }
        }
        var permissions = permissionBag.DistinctBy(e => e.Key);
        profile.Quyen = permissions;

        profile.IsSuperAdmin = identityService.IsSuperAdmin();

        return profile;
    }
}
