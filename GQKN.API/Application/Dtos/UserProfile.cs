namespace PVI.GQKN.API.Application.Dtos;

public class UserProfile
{

    public int Id { get; set; }

    public string Guid { get; set; }

    public string HoTen { get; set; }

    public string AnhDaiDien { get; set; }

    public DateTime? NgaySinh { get; set; } = null;

    public string DiaChi { get; set; }

    public string SoDienThoai { get; set; }

    public string ChucDanhTenChucDanh { get; set; }

    public int? ChucDanhId { get; set; }

    public int? PhongBanId { get; set; }

    public string PhongBanTenPhongBan { get; set; }

    public bool QuanTri { get; set; } = false;

    public bool TrangThaiUser { get; set; } = true;

    public AccountType AccountType { get; set; } = AccountType.PIAS;

    public DateTime NgayTao { get; set; } = DateTime.Now;

    public DateTime? NgaySua { get; set; }

    public string MaUserPVI { get; set; }

    public IEnumerable<VaiTroInfo> VaiTro { get; set; }

    public IEnumerable<AclOperation> Quyen { get; set; }

    public bool IsSuperAdmin { get; set; }

}
