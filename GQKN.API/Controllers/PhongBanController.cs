namespace PVI.GQKN.API.Controllers;

/// <summary>
/// Quản lý danh sách đơn vị tham gia vào hệ thống
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class PhongBanController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PhongBanController> _logger;
    private readonly GQKNDbContext _context;
    private readonly IMapper _mapper;

    public PhongBanController(IMediator mediator,
        ILogger<PhongBanController> logger,
        IMapper mapper,
        GQKNDbContext context)
    {
        this._mediator = mediator ?? throw new ArgumentNullException(nameof(IMediator));
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        this._logger = logger;
        this._context = context;
    }

    /// <summary>
    /// Query danh sách phòng ban
    /// </summary>
    /// <returns>
    /// Danh sách phòng ban
    /// </returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.PHONGBAN_LIST)]
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<PhongBanDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get([FromQuery] QueryPhongBanCommand request)
    {
        var result = await _mediator.Send(request);

        var pageData = result.ToPage<PhongBanDto>(_mapper);

        return Ok(pageData);
    }

    /// <summary>
    /// Query thông tin phòng ban
    /// </summary>
    /// <param name="id">Id đơn vị</param>
    /// <response code="200">Trả về thông tin đơn vị</response>
    /// <response code="404">Không tìm thấy đơn vị</response>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.PHONGBAN_LIST)]
    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(PhongBanDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var item = await _mediator.Send(new GetPhongBanByIdCommand() { Id = id });

        if (item == null)
            return NotFound();

        return Ok(_mapper.Map<PhongBanDto>(item));
    }

    /// <summary>
    /// Tạo mới đơn vị tham gia hệ thống
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.PHONGBAN_CREATE)]
    [HttpPost]
    [ProducesResponseType(typeof(PhongBanDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreatePhongBanCommand request)
    {
        var item = await _mediator.Send(request);
        if (item == null)
        {
            return BadRequest();
        }

        var dto = _mapper.Map<PhongBanDto>(item);

        return Ok(dto);
    }

    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.PHONGBAN_UPDATE)]
    [Route("{id}")]
    [HttpPut]
    [ProducesResponseType(typeof(PhongBanDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Put(string id, [FromBody] UpdatePhongBanCommand command)
    {
        command._Id = id;

        var item = await this._mediator.Send(command);

        if (item == null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<PhongBanDto>(item));
    }

    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.PHONGBAN_DELETE)]
    [Route("{id}")]
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeletePhongBanCommand() { Id = id };

        var result = await this._mediator.Send(command);
        if (result)
        {
            return Ok();
        }

        return NotFound();
    }

}
