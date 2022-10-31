using PVI.GQKN.API.Application.Commands.ChucDanhCommands;
using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ChucDanhController : ControllerBase
{
    private readonly IChucDanhRepository chucDanhRepository;
    private readonly ILogger<ChucDanhController> logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public ChucDanhController(IMapper mapper,
        IMediator mediator,
        IChucDanhRepository chucDanhRepository, 
        ILogger<ChucDanhController> logger)
    {
        this.chucDanhRepository = chucDanhRepository;
        this.logger = logger;
        this._mediator = mediator ?? throw new ArgumentNullException(nameof(IMediator));
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
    }

    /// <summary>
    /// Danh sách chức danh người dùng trong tổ chức
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.CHUCDANH_LIST)]
    [ProducesResponseType(typeof(IEnumerable<ChucDanh>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult>  Get()
    {
        var entities = await this.chucDanhRepository.GetAll().ToListAsync();

        return Ok(entities);
    }

    /// <summary>
    /// Thông tin chi tiết chức danh người dùng với mã <para>id</para>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ClaimAuthorize(ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.CHUCDANH_LIST)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ChucDanh), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var item = await this.chucDanhRepository.GetByGuidAsync(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    // POST api/<ChiNhanhController>
    [HttpPost]
    [ProducesResponseType(typeof(ChucDanhInfo), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateChucDanhCommand command)
    {
        var item = await _mediator.Send(command);

        if (item == null)
            return BadRequest();

        //return CreatedAtAction(nameof(Get), new { id = item.Id });
        return Ok(item);
    }

    //// PUT api/<ChucDanhController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<ChucDanhController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
}
