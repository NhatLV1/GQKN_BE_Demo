namespace PVI.GQKN.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class EmailTemplateController : ControllerBase
{
	private readonly ILogger<EmailTemplateController> logger;
	private readonly IMediator mediator;

	public EmailTemplateController(ILogger<EmailTemplateController> logger, 
		IMediator mediator)
	{
		this.logger = logger;
		this.mediator = mediator;
	}

	//[HttpGet]
	//[ProducesResponseType((int)HttpStatusCode.OK)]
	//public async Task<IActionResult> Get()
	//{ 
	//	//var result = await this.mediator.Send(new )
	//}
}
