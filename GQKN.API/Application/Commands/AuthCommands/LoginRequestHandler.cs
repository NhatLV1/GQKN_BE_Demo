namespace PVI.GQKN.API.Application.Commands.AuthCommands;

public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<LoginRequestHandler> logger;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public LoginRequestHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<LoginRequestHandler> logger,
        IMediator mediator,
        IMapper mapper
        )
    {
        this._userManager = userManager;
        this.logger = logger;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public async Task<LoginResult> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        LoginResult loginResult;

        var command = mapper.Map<GQKNLoginRequest>(request);
        loginResult = await mediator.Send(command);

        //var user = await _userManager.FindByNameAsync(request.Username);
        //if (user == null)
        //{
        //    // try pias login
        //    var piasLoginCommand = mapper.Map<PiasLoginRequest>(request);
        //    loginResult = await mediator.Send(piasLoginCommand);
        //}
        //else
        //{
        //    if (user.AccountType == AccountType.GQKN)
        //    {
        //        var command = mapper.Map<GQKNLoginRequest>(request);
        //        loginResult = await mediator.Send(command);
        //    }
        //    else
        //    {
        //        var command = mapper.Map<PiasLoginRequest>(request);
        //        loginResult = await mediator.Send(command);
        //    }
        //}

        return loginResult;
    }
}
