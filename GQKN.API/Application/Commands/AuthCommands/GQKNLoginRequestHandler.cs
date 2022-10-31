namespace PVI.GQKN.API.Application.Commands.AuthCommands;

public class GQKNLoginRequestHandler : IRequestHandler<GQKNLoginRequest, LoginResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LoginRequestHandler> logger;
    private readonly IAuthService authService;

    public GQKNLoginRequestHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<LoginRequestHandler> logger,
        IAuthService authService
        )
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this.logger = logger;
        this.authService = authService;
    }

    public async Task<LoginResult> Handle(GQKNLoginRequest request, CancellationToken cancellationToken)
    {
        LoginResult loginResult;

        var signInResult = await _signInManager.PasswordSignInAsync(
                 request.Username,
                 request.Password,
                 isPersistent: false,
                 lockoutOnFailure: false);

        if (signInResult.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            loginResult = new LoginResult()
            {
                Successed = true,
                Token = await this.authService.GenerateJwtToken(user)
            };
        }
        else
        {
            loginResult = new LoginResult()
            {
                Successed = false,
                IsLockedOut = signInResult.IsLockedOut,
                IsNotAllowed = signInResult.IsNotAllowed,
                RequiresTwoFactor = signInResult.RequiresTwoFactor,
            };
        }

        return loginResult;
    }
}
