namespace PVI.GQKN.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
#if !DEBUG
[Authorize]
#endif
public class DanhMucController : ControllerBase
{
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IPhongBanRepository phongBanRepository;
    private readonly IChucDanhRepository chucDanhRepository;
    private readonly IDonViRepository donViRepository;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public DanhMucController(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IPhongBanRepository phongBanRepository,
        IChucDanhRepository chucDanhRepository,
        IDonViRepository donViRepository,
        IMediator mediator,
        IMapper mapper
        )
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
        this.phongBanRepository = phongBanRepository;
        this.chucDanhRepository = chucDanhRepository;
        this.donViRepository = donViRepository;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    /// <summary>
    /// Danh sách phòng ban
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("phongban")]
    [ProducesResponseType(typeof(IEnumerable<PhongBanInfo>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPhongBan()
    {
        var entities = await this.phongBanRepository.GetAll().ToListAsync();
        var dtos = this.mapper.Map<IEnumerable<PhongBanInfo>>(entities);
        return Ok(dtos);
    }

    /// <summary>
    /// Danh sách chức danh
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("chucdanh")]
    [ProducesResponseType(typeof(IEnumerable<ChucDanhInfo>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetChucDanh()
    {
        var entities = await this.chucDanhRepository.GetAll().ToListAsync();
        var dtos = this.mapper.Map<IEnumerable<ChucDanhInfo>>(entities);
        return Ok(dtos);
    }

    /// <summary>
    /// Danh sách user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("user")]
    [ProducesResponseType(typeof(IEnumerable<UserInfo>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUsers()
    {
        var entities = await userManager.Users.ToListAsync();
        var dtos = this.mapper.Map<IEnumerable<UserInfo>>(entities);
        return Ok(dtos);
    }

    /// <summary>
    /// Danh mục vai trò (roles)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("vaitro")]
    [ProducesResponseType(typeof(IEnumerable<VaiTroInfo>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetVaiTro()
    {
        var entities = await roleManager.Roles.ToListAsync();
        var dtos = this.mapper.Map<IEnumerable<VaiTroInfo>>(entities);
        return Ok(dtos);
    }

    /// <summary>
    /// Danh mục quyền (permissions)
    /// </summary>
    /// <param name="authService"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("acl")]
    [ProducesResponseType(typeof(IEnumerable<QuyenDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetACL([FromServices] IAuthService authService)
    {
        var acls = await mediator.Send(new AclListRequest() { });
        var dtos = mapper.Map<IEnumerable<QuyenDto>>(acls);
        return Ok(dtos);
    }

    /// <summary>
    /// Danh sách Scope. Mỗi scope quy định các Resource liên quan; 
    /// danh sách quyền tương ứng với mỗi Resource.
    /// </summary>
    /// <param name="authService"></param>
    /// <returns></returns>
    [Route("scope")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ScopeDto>), (int)HttpStatusCode.OK)]
    public IActionResult GetACL2()
    {
        var command = new GetScopeRequest() { };
        var scopeDtos = mediator.Send(command);
        return Ok(scopeDtos);
    }

    ///// <summary>
    ///// Danh sách Scope. 
    ///// </summary>
    ///// <param name="authService"></param>
    ///// <returns></returns>
    //[Route("scopes")]
    //[HttpGet]
    //[ProducesResponseType(typeof(IEnumerable<ScopeDto>), (int)HttpStatusCode.OK)]
    //public IActionResult GetScopes([FromServices] IAuthService authService)
    //{
    //    var scopes = authService.GetScopes();
      
    //    return Ok(scopes);
    //}


    /// <summary>
    /// Danh sách đơn vị
    /// </summary>
    /// <param name="authService"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("donvi")]
    [ProducesResponseType(typeof(IEnumerable<DonViDto>), (int)HttpStatusCode.OK)]
    public IActionResult GetDonVi([FromServices] IAuthService authService)
    {
        var entities = donViRepository.GetAll();
        var dtos = mapper.Map<IEnumerable<DonViDto>>(entities);
        return Ok(dtos);
    }
}
