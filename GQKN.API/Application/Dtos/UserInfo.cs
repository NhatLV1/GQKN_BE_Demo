namespace PVI.GQKN.API.Application.Dtos;

public class UserInfo
{
    public string Guid { get; set; }

    public int Id { get; set; }

    public string Username { get; set; }

    public string HoTen { get; set; }

    public string Email { get; set; }

    public string SoDienThoai { get; set; }

    public string MaUserPVI { get; set; }
    public string Vung { get; set; }
    public int? ChucDanhId { get; set; }
    public int? PhongBanId { get; set; }
    public int? DonViId { get; set; }

    public IEnumerable<VaiTroInfo> VaiTro { get; set; }

    public bool TrangThaiUser { get; set; }
}
