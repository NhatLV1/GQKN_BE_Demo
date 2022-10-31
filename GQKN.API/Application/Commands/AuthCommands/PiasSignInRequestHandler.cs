namespace PVI.GQKN.API.Application.Commands.AuthCommands;

public class PiasSignInRequestHandler : IRequestHandler<PiasSignInRequest, PiasSignInResult>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ILogger<PiasSignInRequestHandler> logger;
    private readonly IAuthPVI piasAuthService;

    public PiasSignInRequestHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<PiasSignInRequestHandler> logger,
        IAuthPVI piasAuthService
        )
    {
        this.userManager = userManager;
        this.logger = logger;
        this.piasAuthService = piasAuthService;
    }

    public async Task<PiasSignInResult> Handle(PiasSignInRequest request, CancellationToken cancellationToken)
    {
        PiasSignInResult signInResult;
        var user = await userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            var loginType = request.Username.IsValidEmailAddress() ?
                IAuthPVI.LOGIN_TYPE_MAIL : IAuthPVI.LOGIN_TYPE_QLCD;

            var loginPiasResult = await piasAuthService.Login(request.Username, request.Password, loginType);

            if (loginPiasResult.Successed)
            {
                user = new ApplicationUser()
                {
                    UserName = request.Username,
                };

                var identityResult = await userManager.CreateAsync(user);
                if (!identityResult.Succeeded)
                {
                    logger.LogError(identityResult.ToString());
                    throw new GQKNDomainException(identityResult.ToString());
                }

                signInResult = new PiasSignInResult()
                {
                    Successed = true,
                };
            }
            else
            {
                signInResult = new PiasSignInResult()
                {
                    Successed = false,
                };
            }
        }
        else
        {
            signInResult = new PiasSignInResult()
            {
                Successed = false,
            };
        }

        return signInResult;
    }
}
