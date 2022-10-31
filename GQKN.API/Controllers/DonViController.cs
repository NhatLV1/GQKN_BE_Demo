namespace PVI.GQKN.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class DonViController : ControllerBase
{
    private readonly IDonViRepository donViRepository;
    private readonly ILogger<DonViController> logger;
    private readonly IMediator mediator;

    public DonViController(IDonViRepository donViRepository,
        ILogger<DonViController> logger,
        IMediator mediator)
    {
        this.donViRepository = donViRepository;
        this.logger = logger;
        this.mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách đơn vị
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DonVi>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var entities = await this.donViRepository
            .GetAll("Scopes")
            .ToListAsync();
        return Ok(entities);
    }

    /// <summary>
    /// Thông tin chi tiết đơn vị
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(DonVi), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get(string id)
    { 
        var entity = await this.donViRepository.GetByGuidAsync(id);
        
        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    /// <summary>
    /// Tạo đơn vị mới
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(DonVi), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateDonViRequest request)
    {
        var entity = await mediator.Send(request);
        return Ok(entity);
    }

    /// <summary>
    /// Cập nhật đơn vị
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [Route("{id}")]
    [HttpPut]
    [ProducesResponseType(typeof(DonVi), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateDonViRequest request)
    {
        request.Id = id;
        var result = await mediator.Send(request);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
