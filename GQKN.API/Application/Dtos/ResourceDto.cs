namespace PVI.GQKN.API.Application.Dtos;

public class ResourceDto
{
    public string ResourceId { get; set; }
    public int ResourceOrder { get; set; }
    public string ResourceName { get; set; }
    public string ScopeId { get; set; }

    public ResourceDto(string id, int order, string name, string scopeId)
    {
        ResourceId = id;
        ResourceOrder = order;
        ResourceName = name;
        ScopeId = scopeId;
    }

    public IEnumerable<PermissionDto> Operations { get; set; }
}