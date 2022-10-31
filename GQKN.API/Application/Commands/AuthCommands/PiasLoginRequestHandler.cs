using PVI.GQKN.API.Services;

namespace PVI.GQKN.API.Application.Commands.AuthCommands;

public class PiasLoginRequestHandler : IRequestHandler<PiasLoginRequest, LoginResult>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IAuthService authService;
    private readonly IAuthPVI piasAuthService;
    private readonly IPhongBanRepository phongBanRepository;
    private readonly IDonViRepository donViRepository;
    private readonly ILogger<PiasLoginRequestHandler> logger;
    private readonly IMapper mapper;

    public PiasLoginRequestHandler(
        UserManager<ApplicationUser> userManager,
        IAuthService authService,
        IAuthPVI piasAuthService,
        IPhongBanRepository phongBanRepository,
        IDonViRepository donViRepository,
        ILogger<PiasLoginRequestHandler> logger,
        IMapper mapper
        )
    {
        this.userManager = userManager;
        this.authService = authService;
        this.piasAuthService = piasAuthService;
        this.phongBanRepository = phongBanRepository;
        this.donViRepository = donViRepository;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<LoginResult> Handle(PiasLoginRequest request, CancellationToken cancellationToken)
    {
        var loginType = request.Username.IsValidEmailAddress() ?
            IAuthPVI.LOGIN_TYPE_MAIL : IAuthPVI.LOGIN_TYPE_QLCD;

        var loginPiasResult = await piasAuthService.Login(request.Username, request.Password, loginType);
        LoginResult loginResult = new LoginResult()
        {
            Successed = loginPiasResult.IsSuccessed,
            Message = loginPiasResult.Message,
            Status = loginPiasResult.Status
        };

        if (loginPiasResult.IsSuccessed)
        {
            var user = await userManager.FindByNameAsync(request.Username);
          
            if (user == null)
            {
                var piasUser = loginPiasResult.DataUser;
                user = this.mapper.Map<ApplicationUser>(piasUser);
                user.UserName = request.Username;
                user.PhongBan = await this.phongBanRepository
                    .FirstOrDefaultAsync(e => e.MaPhongBan == piasUser.ma_phong);
                // TODO: đồng bộ các thông tin khác: chức vụ, ...

                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return loginResult;
                }
            }

            loginResult = new LoginResult()
            {
                Successed = true,
                Token = await authService.GenerateJwtToken(user),
                Message = loginPiasResult.Message,
                Status = loginPiasResult.Status
            };
        }

        return loginResult;
    }
}
