namespace PVI.GQKN.API.Application.Commands.UserCommands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ILogger<DeleteUserCommandHandler> logger;
    private readonly IAuthPVI authPVIService;

    public DeleteUserCommandHandler(
        UserManager<ApplicationUser> userManager, 
        IAuthPVI authPVIService,
        ILogger<DeleteUserCommandHandler> logger)
    {
        this.userManager = userManager;
        this.logger = logger;
        this.authPVIService = authPVIService;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id);

        if (user != null)
        {
            var result = await userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                if (!string.IsNullOrEmpty(user.MaUserPVI))
                {
                    var piasRequestResult =
                         await this.authPVIService.DeleteUser(user.MaUserPVI);

                    if (!piasRequestResult.Successed)
                    {
                        logger.LogError(piasRequestResult.Message);
                        throw new GQKNDomainException(piasRequestResult.Message);
                    }
                }
            }
           

            return result.Succeeded;
        }

        return false;
    }

  
}
