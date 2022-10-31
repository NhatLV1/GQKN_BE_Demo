namespace PVI.GQKN.API.Application.Dtos;

public class ScopeDto
{
    public int ScopeOrder { get; set; }
    public string ScopeId { get; set; }
    public string ScopeName { get; set; }

    public ScopeDto(int order, string id, string name)
    {
        ScopeOrder = order;
        ScopeId = id;
        ScopeName = name;
    }

    public IEnumerable<ResourceDto> Resources { get; set; }
}
