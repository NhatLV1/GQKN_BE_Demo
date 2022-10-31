using Duende.IdentityServer.Models;

namespace PVI.GQKN.API.Controllers;

/// <summary>
/// Quản lý vai trò trong hệ thống GQKN
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class VaiTroController : ControllerBase
{
    private readonly ILogger<VaiTroController> logger;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly IAuthService authService;

    public VaiTroController(
        ILogger<VaiTroController> logger,
        RoleManager<ApplicationRole> roleManager,
        IMediator mediator,
        IMapper mapper,
        IAuthService authService)
    {
        this.logger = logger;
        this.roleManager = roleManager;
        this.mediator = mediator;
        this.mapper = mapper;
        this.authService = authService;
    }

    /// <summary>
    /// Lấy danh sách vai trò
    /// </summary>
    /// <returns></returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.VAITRO_LIST)]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VaiTroInfo>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var roles = await roleManager.Roles.ToListAsync();

        var dtos = mapper.Map<IEnumerable<VaiTroInfo>>(roles);
        return Ok(dtos);
    }

    /// <summary>
    /// Chi tiết vai trò
    /// </summary>
    /// <param name="id">Mã id của vai trò</param>
    /// <returns></returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.VAITRO_LIST)]
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(VaiTroDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get(string id)
    {
        var role = await roleManager.FindByIdAsync(id);

        if (role == null)
            return NotFound();

        var dto = mapper.Map<VaiTroDto>(role);
        var acls = authService.GetACL();
        
        var permissions = await roleManager.GetPermissionIdsAsync(role, acls);
        var dtoResources = permissions.ToResourceGroups(acls);

        dto.QuyenIds = dtoResources.ToList();

        return Ok(dto);
    }

    /// <summary>
    /// Tạo vai trò người dùng
    /// </summary>
    /// <param name="command">Thông tin vai trò</param>
    /// <returns></returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.VAITRO_CREATE)]
    [HttpPost]
    [ProducesResponseType(typeof(VaiTroDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateVaiTroRequest command)
    {
        var role = await mediator.Send(command);
        
        if (role == null)
            return BadRequest();

        var dto = mapper.Map<VaiTroDto>(role);

        var acls = authService.GetACL();
        var permissions = await roleManager.GetPermissionIdsAsync(role, acls);

        dto.QuyenIds = permissions.ToResourceGroups(acls);

        return Ok(dto);
    }

    /// <summary>
    /// Cập nhật vai trò người dùng
    /// </summary>
    /// <param name="id">Mã vai trò</param>
    /// <param name="command">Thông tin vai trò</param>
    /// <returns></returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.VAITRO_UPDATE)]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(VaiTroDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateVaiTroRequest command)
    {
        command.Id = id;
        var role = await mediator.Send(command);

        if (role == null)
            return BadRequest();

        var dto = mapper.Map<VaiTroDto>(role);
        var acls = authService.GetACL();
        var permissions = await roleManager.GetPermissionIdsAsync(role, authService.GetACL());
        
        dto.QuyenIds = permissions.ToResourceGroups(acls);

        return Ok(dto);
    }

    /// <summary>
    /// Xóa vai trò người dùng. (Vai trò này cũng sẽ được xóa khỏi những user đang sử dụng vai trò này).
    /// </summary>
    /// <param name="id">Mã vai trò cần xóa</param>
    /// <returns></returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.VAITRO_DELETE)]
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteVaiTroRequest() { Id = id };
        
        var result = await mediator.Send(command);

        if (result)
        {
            return Ok();
        }

        return NotFound();
    }


}
