namespace PVI.GQKN.API.Application.Dtos;

public class DonViDto
{
    public int Id { get; set; }
    public string Guid { get; set; }
    public string MaDonVi { get; set; }
    public string TenDonVi { get; set; }
    public string MaTinh { get; set; }

    public IEnumerable<Scope> Scopes { get; set; }
}
