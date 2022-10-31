namespace PVI.GQKN.API.Application.Dtos;

public class QuyenDto
{
    [Required]
    public string Key { get; set; }

    public string Name { get;  set; }

    public string Scope { get;  set; }

    public string Resource { get;  set; }

    public string Code { get; set; }
}
