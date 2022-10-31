namespace PVI.GQKN.API.Application.Commands.DanhMucCommands;

public class GetScopeRequest: IRequest<IEnumerable<ScopeDto>>
{
}

public class GetScopeRequestHandler :
    IRequestHandler<GetScopeRequest, IEnumerable<ScopeDto>>
{
    private readonly IAuthService authService;
    private readonly IMapper mapper;

    public GetScopeRequestHandler(
        IAuthService authService, 
        IMapper mapper)
    {
        this.authService = authService;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ScopeDto>> Handle(GetScopeRequest request, CancellationToken cancellationToken)
    {
        var acls = authService.GetACL();
        var resources = authService.GetResources();

        // filter out scope
        var userScopes = await authService.GetUserScopes();
        var scopes = authService.GetScopes().Where(e => userScopes.Contains(e.Id));

        var scopeDtos = mapper.Map<IEnumerable<ScopeDto>>(scopes);
        
        foreach (var scope in scopeDtos)
        {
            var resourceDtos = (from r in resources
                                where r.ScopeId == scope.ScopeId
                                select new ResourceDto(r.Id, r.Order, r.Name, r.ScopeId)).ToList();
            foreach (var r in resourceDtos)
            {
                var aclDtos = (from a in acls
                               where a.Resource == r.ResourceId
                               select new PermissionDto()
                               {
                                   Key = a.Key,
                                   PermissionName = a.Name,
                                   Code = a.Code,
                               }).ToList();
                r.Operations = aclDtos;
            }
            scope.Resources = resourceDtos;
        }

        return scopeDtos;
    }
}
