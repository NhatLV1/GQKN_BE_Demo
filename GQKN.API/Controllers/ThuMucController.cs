using Microsoft.AspNetCore.Mvc;
using PVI.GQKN.API.Application.Commands.ThuMucCommands;

namespace PVI.GQKN.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ThuMucController : ControllerBase
    {
        private readonly IThuMucRepositoty _thuMucRepositoty;
        private readonly ILogger<ThuMucController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ThuMucController(IThuMucRepositoty thuMucRepositoty,
            ILogger<ThuMucController> logger,
            IMapper mapper,
            IMediator mediator)
        {
            _thuMucRepositoty = thuMucRepositoty;
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ThuMuc>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var entities = await _thuMucRepositoty.GetAll().ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{ma_kbtt}")]
        [ProducesResponseType(typeof(PagedList<ThuMuc>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] int ma_kbtt)
        {
            var data =  _thuMucRepositoty.Get(filter: p => p.HoSoTonThatId == ma_kbtt).ToList();
            //var pageData = result.ToPage<ThuMucDto>(mapper);

            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ThuMucDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateThuMucCommand request)
        {
            var item = await _mediator.Send(request);
            if (item == null)
            {
                return BadRequest();
            }

            var dto = _mapper.Map<ThuMucDto>(item);

            return Ok(dto);
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(ThuMucDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateThuMucCommand command)
        {
            command._Id = id;

            var item = await _mediator.Send(command);

            if (item == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<ThuMucDto>(item));
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeletePhongBanCommand() { Id = id };

            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
