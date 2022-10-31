namespace PVI.GQKN.API.Application.Commands.UserCommands;

public class UnlockUserRequestHandler: IRequestHandler<UnlockUserRequest, bool>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserRepository userRepository;
    private readonly ILogger<UnlockUserRequestHandler> logger;

    public UnlockUserRequestHandler(
        UserManager<ApplicationUser> userManager,
        IUserRepository userRepository,
        ILogger<UnlockUserRequestHandler> logger)
    {
        this.userManager = userManager;
        this.userRepository = userRepository;
        this.logger = logger;
    }

    public async Task<bool> Handle(UnlockUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FirstOrDefaultAsync(e => e.Id == request.UserId);

        if (user == null)
            return false;

        user.ActiveUser();

        return await userRepository.UnitOfWork.SaveEntitiesAsync();
    }
}
