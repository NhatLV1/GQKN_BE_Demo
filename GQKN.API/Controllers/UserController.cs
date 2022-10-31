namespace PVI.GQKN.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly IMapper mapper;
    private readonly IMediator mediator;
    private readonly IApplicationRoleRepository applicationRoleRepository;
    private readonly IUserRepository userRepository;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IIdentityService _identityService;
    private readonly IUserQueries applicationUserQueries;

    public UserController(
        ILogger<UserController> logger,
        IMapper mapper,
        IMediator mediator,
        IApplicationRoleRepository applicationRoleRepository,
        IUserRepository userRepository,
        UserManager<ApplicationUser> userManager,
        IIdentityService identityService,
        IUserQueries applicationUserQueries)
    {
        this._identityService = identityService;
        this.logger = logger;
        this.mapper = mapper;
        this.mediator = mediator;
        this.applicationRoleRepository = applicationRoleRepository;
        this.userRepository = userRepository;
        this.userManager = userManager;
        this.applicationUserQueries = applicationUserQueries;
    }

    /// <summary>
    /// Danh sách tài khoản người dùng
    /// </summary>
    /// <returns></returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.ACCOUNT_LIST)]
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<UserInfo>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get([FromQuery] UserParams request)
    {
        var listedPage = await userRepository.GetPage(request,
            "ChucDanh,PhongBan");

        var data = new List<UserInfo>();
        foreach (var user in listedPage.Data)
        {
            var userDto = mapper.Map<UserInfo>(user);
            var userRoles = await this.userManager.GetRolesAsync(user);
            var vaiTros = await this.applicationRoleRepository.FindRolesWithNames(userRoles);
            userDto.VaiTro = mapper.Map<IEnumerable<VaiTroInfo>>(vaiTros);

            data.Add(userDto);
        }

        var dto = new
        {
            listedPage.TotalCount,
            listedPage.PageSize,
            listedPage.TotalPage,
            listedPage.NextPageId,
            listedPage.PageId,
            data
        };

        return Ok(dto);
    }

    /// <summary>
    /// Chi tiết tài khoản người dùng
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.ACCOUNT_LIST)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var user = await applicationUserQueries.GetById(id);

        if (user == null)
            return NotFound();

        var userDto = await MapToUserDtoAsync(user);

        return Ok(userDto);
    }

    private async Task<UserDto> MapToUserDtoAsync(ApplicationUser user)
    {
        var userDto = mapper.Map<UserDto>(user);

        var userRoles = await this.userManager.GetRolesAsync(user);
        var vaiTros = await this.applicationRoleRepository.FindRolesWithNames(userRoles);
        userDto.VaiTro = mapper.Map<IEnumerable<VaiTroInfo>>(vaiTros);

        return userDto;
    }

    //[ClaimAuthorize(ADMIN_OPS.ACL_GROUP, ADMIN_OPS.ACCOUNT_CREATE)]
    //[HttpPost]
    //[ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
    //{
    //    var userEntity = await mediator.Send(command);

    //    if(userEntity == null)
    //        return BadRequest();

    //    var userDto = mapper.Map<UserDto>(userEntity);
    //    return Ok(userDto);
    //}

    /// <summary>
    /// Cập nhật vai trò tài khoản người dùng
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.ACCOUNT_UPDATE)]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateUserRequest command)
    {
        command.__UserId = id;

        var user = await mediator.Send(command);

        if (user == null)
            return BadRequest();

        var userDto = await MapToUserDtoAsync(user);

        return Ok(userDto);
    }

    /// <summary>
    /// Tạm dừng tài khoản GQKN
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Route("tamdung")]
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.ACCOUNT_DEACTIVATE)]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Deactivation([FromBody] LockUserRequest command)
    {
        var result = await mediator.Send(command);

        if (result)
        {
            var user = await userManager.FindByIdAsync(command.UserId);
            var dto = await MapToUserDtoAsync(user);
            return Ok(dto);
        }

        return NotFound();
    }

    /// <summary>
    /// Kích hoạt tài khoản GQKN
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Route("kichhoat")]
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.ACCOUNT_DEACTIVATE)]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Activate([FromBody] UnlockUserRequest command)
    {
        var result = await mediator.Send(command);

        if (result)
        {
            var user = await userManager.FindByIdAsync(command.UserId);
            var dto = await MapToUserDtoAsync(user);
            return Ok(dto);
        }

        return NotFound();
    }

    /// <summary>
    /// Tạo tài khoản user GQKN
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.ACCOUNT_CREATE)]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
    {
        var result = await mediator.Send(request);

        if (result != null)
        {
            var dto = await MapToUserDtoAsync(result);
            return Ok(dto);
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteUserCommand() { Id = id };

        var result = await mediator.Send(command);

        if (result)
        {
            return Ok();
        }

        return NotFound();
    }



    /// <summary>
    /// Cập nhật mật khẩu user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [Route("update-password")]
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.ACCOUNT_UPDATE)] // TODO: Update permission!!
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdatePasword([FromBody] SetUserPasswordRequest request)
    {
        await mediator.Send(request);
        return Ok();
    }

    ///<summary>
    ///Đổi mật khẩu
    /// </summary>
    /// <param name="request"></param>
    /// <return></return>
    [Route("change-password")]
    //[ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.ACCOUNT_UPDATE)] // TODO: Update permission!!
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [HttpPost]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        string userId = null;
        try
        {
            var b = _identityService.GetUserName();
            var c = await userManager.FindByNameAsync(b);
            userId = c.Id.ToString();
        }
        catch
        {

        }
        if(userId != null)
        {
            request.UserId = userId.ToString();
        }
        await mediator.Send(request);
        return Ok();

    }

    [Route("forgot-password")]
    //[ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.ACCOUNT_UPDATE)] // TODO: Update permission!!
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [HttpPost]
    public async Task<IActionResult> Forgot([FromBody] ForgotPasswordRequest request)
    {
        string userId = null;
        try
        {
            var b = _identityService.GetUserName();
            var c = await userManager.FindByNameAsync(b);
            userId = c.Id.ToString();
        }
        catch
        {

        }
        if (userId != null)
        {
            request.UserId = userId.ToString();
        }

        var result = await mediator.Send(request);
        return Ok(result);

    }

    [Route("forgot-password-confirm")]
    
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [HttpPost]
    public async Task<IActionResult> ForgotConfirm([FromBody] ForgotPasswordConfirmRequest request)
    {
        string userId = null;
        try
        {
            var b = _identityService.GetUserName();
            var c = await userManager.FindByNameAsync(b);
            userId = c.Id.ToString();
        }
        catch
        {

        }
        if (userId != null)
        {
            request.UserId = userId.ToString();
        }
        await mediator.Send(request);
        return Ok();

    }

}
