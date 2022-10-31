namespace PVI.GQKN.API.Application.Commands.UserCommands;

public class LockUserRequestHandler : IRequestHandler<LockUserRequest, bool>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserRepository userRepository;
    private readonly ILogger<LockUserRequestHandler> logger;

    public LockUserRequestHandler(
        UserManager<ApplicationUser> userManager, 
        IUserRepository userRepository,
        ILogger<LockUserRequestHandler> logger)
    {
        this.userManager = userManager;
        this.userRepository = userRepository;
        this.logger = logger;
    }

    public async Task<bool> Handle(LockUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FirstOrDefaultAsync(e => e.Id == request.UserId);
        
        if(user == null) 
            return false;

        user.Deactive();

        return await userRepository.UnitOfWork.SaveEntitiesAsync();
    }
}
