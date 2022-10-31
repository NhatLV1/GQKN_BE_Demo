namespace PVI.GQKN.API.Application.Commands.UserCommands;

public class SetUserPasswordRequestHandler : IRequestHandler<SetUserPasswordRequest, bool>
{
    private readonly IAuthPVI authPVI;
    private readonly IIdentityService identityService;
    private readonly UserManager<ApplicationUser> userManager;

    public SetUserPasswordRequestHandler(
        IAuthPVI authPVI,
        IIdentityService identityService,
        UserManager<ApplicationUser> userManager)
    {
        this.authPVI = authPVI;
        this.identityService = identityService;
        this.userManager = userManager;
    }

    public async Task<bool> Handle(SetUserPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new GQKNDomainException("User not found");
        }

        var piasRequest = new UpdatePVIUserRequest()
        {
            UserId = user.MaUserPVI,
            Fullname = user.HoTen,
            Password = request.Password
        };

        var result = await authPVI.UpdateUser(piasRequest);
        if (!result.Successed)
        {
            throw new GQKNDomainException(result.Status, result.Message);
        }

        return true;
    }
}
