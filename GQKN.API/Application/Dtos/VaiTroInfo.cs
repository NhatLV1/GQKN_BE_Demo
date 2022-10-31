namespace PVI.GQKN.API.Application.Dtos;

public class VaiTroInfo
{
    /// <summary>
    /// Key phụ
    /// </summary>
    public string id { get; private set; } 
   
    /// <summary>
    /// Tên vai trò
    /// </summary>
    public string TenVaiTro { get; private set; } // tên vai trò

    /// <summary>
    /// Mã đơn vị quản lý
    /// </summary>
    public int? DonViId { get; set; }

    /// <summary>
    /// Mã phòng ban quản lý
    /// </summary>
    public int? PhongBanId { get; set; }

    /// <summary>
    /// Ngày tạo
    /// </summary>
    public DateTime NgayTao { get; set; }

    /// <summary>
    /// Ngày sửa
    /// </summary>
    public DateTime? NgaySua { get; set; }

}
