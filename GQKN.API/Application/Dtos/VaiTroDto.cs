namespace PVI.GQKN.API.Application.Dtos;

public class ResourceGroupDto
{
    public string ResourceId { get; set; }
    public List<string> QuyenIds { get; set; }
}

public class VaiTroDto
{
    /// <summary>
    /// Key phụ
    /// </summary>
    public string Id { get; set; } // Key phụ
 
    /// <summary>
    /// Tên vai trò
    /// </summary>
    public string TenVaiTro { get; set; } // Tên vai trò
    /// <summary>
    /// Mã phòng ban
    /// </summary>
    public int? PhongBanId { get; set; }

    /// <summary>
    /// Mã đơn vị
    /// </summary>
    public int? DonViId { get;  set; }

    /// <summary>
    /// Ngày tạo
    /// </summary>
    public DateTime NgayTao { get; set; }
    /// <summary>
    /// Ngày sửa
    /// </summary>
    public DateTime NgaySua { get; set; }

    public List<ResourceGroupDto> QuyenIds { get; set; }
}
