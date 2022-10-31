namespace PVI.GQKN.API.Application.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public Guid Guid { get; set; }
    public string Email { get; set; }
    public string HoTen { get; set; }
    public string AnhDaiDien { get; set; }
    public DateTime? NgaySinh { get; set; }
    public string DiaChi { get; set; }
    public int? ChucDanhId { get; set; }
    public int? PhongBanId { get; set; }
    public int? DonViId { get; set; }

    public string MaUserPVI { get; set; }

    public bool QuanTri { get; set; }
    public bool TrangThaiUser { get; set; }
    public DateTime NgayTao { get; set; }
    public DateTime? NgaySua { get; set; }

    public string SoDienThoai { get; set; }

    /// <summary>
    /// Danh sách vai trò (ids)
    /// </summary>
    public IEnumerable<VaiTroInfo> VaiTro { get; set; }

}