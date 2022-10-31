namespace PVI.GQKN.API.Application.Commands.DanhMucCommands;

public class AclListRequest: IRequest<IEnumerable<AclOperation>>
{

}

public class AclListHandler : IRequestHandler<AclListRequest, IEnumerable<AclOperation>>
{
    private readonly IAuthService authService;

    public AclListHandler(IAuthService authService)
    {
        this.authService = authService;
    }

    public async Task<IEnumerable<AclOperation>> Handle(AclListRequest request, CancellationToken cancellationToken)
    {
        var acls = authService.GetACL();
        var userScopes = await authService.GetUserScopes();

        var result = acls.Where(e => userScopes.Contains(e.Scope));

        return result;
    }
}
