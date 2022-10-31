namespace PVI.GQKN.API.Services.Auth;


public class AclScope
{
    public int Order { get;  private set; }
    public string Id { get; private set; }
    public string Name { get; private set; }
   
    public AclScope(int order, string id, string name)
    {
        Order = order;
        Id = id;
        Name = name;
    }
}

public class AclResource
{
    public string Id { get; private set; }
    public int Order { get; private set; }
    public string Name { get; private set; }
    public string ScopeId { get; private set; }


    public AclResource(int order, string id, string name, string scopeId)
    {
        Order = order;
        Id = id;
        Name = name;
        ScopeId = scopeId;
    }
}

/// <summary>
/// Tác vụ
/// </summary>
public class AclOperation
{
    public string Key => $"{Scope}-{Id}".EncodeBase64();

    public static (ulong id, string nhom) Decode(string key)
    {
        var segs = key.DecodeBase64().Split('-');
        var group = segs[0];
        var id = ulong.Parse(segs[1]);
        return (id, group);
    }

    public int Order { get; private set; }

    public ulong Id { get; private set; }

    public string Name { get; private set; }

    public string Scope { get; private set; }

    public string Resource { get; private set; }
    public string Code { get; private set; }

    public AclOperation(int order, ulong id, string scope, string resource, string name, string code) =>
        (Order, Resource, Id, Name, Scope, Code) = (order, resource, id, name, scope, code);
}

public interface IAuthService
{
    public const string SUPER_ADMIN_CLAIM_NAME = "ROOT";
    public const string SUPER_ADMIN_CLAIM_VALUE = "*";

    public readonly static Claim SUPER_ADMIN_CLAIM = new Claim(SUPER_ADMIN_CLAIM_NAME, SUPER_ADMIN_CLAIM_VALUE);

    public Task<string> GenerateJwtToken(ApplicationUser user);

    public IReadOnlyCollection<AclOperation> GetACL();
    
    public IReadOnlyCollection<AclResource> GetResources();

    public IReadOnlyCollection<AclScope> GetScopes();

    public ulong AggregateClaim(string group);

    public Task<IEnumerable<string>> GetUserScopes();

}
