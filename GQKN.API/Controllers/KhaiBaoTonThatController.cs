namespace PVI.GQKN.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class KhaiBaoTonThatController : ControllerBase
{
	private readonly IMapper mapper;
	private readonly IMediator mediator;

	public KhaiBaoTonThatController(
		IMapper mapper,
		IMediator mediator
		)
	{
		this.mapper = mapper;
		this.mediator = mediator;
	}

	/// <summary>
	/// Lấy danh hồ sơ tổn thất.
	/// </summary>
	/// <param name="command"></param>
	/// <returns></returns>
	[ClaimAuthorize(KBTT_OPS.ACL_SCOPE, KBTT_OPS.LIST_KBTT)]
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<KhaiBaoTonThatKhachHangInfo>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get([FromQuery] QueryKBTTRequest command)
    {
        var result = await this.mediator.Send(command);
		
		var page = result.ToPage<KhaiBaoTonThatKhachHangInfo>(mapper);

        return Ok(page);
    }

    /// <summary>
    /// Tạo khai báo tổn thất (đối tượng khách hàng, môi giới)
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
	[ClaimAuthorize(KBTT_OPS.ACL_SCOPE, KBTT_OPS.CREATE_KBTT)]
    [Route("khachhang")]
	[HttpPost]
	[ProducesResponseType(typeof(KhaiBaoTonThatKhachHangDto), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> CreateKhaiBaoTonThatDonGian(
		[FromBody] CreateKBTTKhachHangRequest request)
	{
		var result = await this.mediator.Send(request);
		if(result != null)
		{
			var dto = this.mapper.Map<KhaiBaoTonThatKhachHangDto>(result);
			return Ok(dto);
		}

		return BadRequest();
	}

	//   [HttpPost]
	//   [ProducesResponseType(typeof(KhaiBaoTonThatDto), (int)HttpStatusCode.OK)]
	//   [ProducesResponseType((int)HttpStatusCode.BadRequest)]
	//   public async Task<IActionResult> CreateKhaiBaoTonThatChiTiet([FromBody] CreateKBTTKhachHangRequest request)
	//   {
	//       var result = await this.mediator.Send(request);
	//       if (result != null)
	//       {
	//           var dto = this.mapper.Map<KhaiBaoTonThatDto>(result);
	//           return Ok(dto);
	//       }

	//       return BadRequest();
	//   }

	//   [HttpPut]
	//   [ProducesResponseType(typeof(KhaiBaoTonThatDto), (int)HttpStatusCode.OK)]
	//   [ProducesResponseType((int)HttpStatusCode.BadRequest)]
	//   public async Task<IActionResult> UpdateKhaiBaoTonThat([FromBody] CreateKhaiBaoTonThatRequest command)
	//   {
	//       var result = await this.mediator.Send(command);
	//       if (result != null)
	//       {
	//           var dto = this.mapper.Map<KhaiBaoTonThatDto>(result);
	//           return Ok(dto);
	//       }

	//       return BadRequest();
	//   }

}
