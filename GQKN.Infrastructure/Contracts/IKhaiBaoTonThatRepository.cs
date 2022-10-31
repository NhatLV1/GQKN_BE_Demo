using System.ComponentModel;

namespace PVI.GQKN.Infrastructure.Contracts;

public class KBTTParams : PagedListQueryParams
{
    /// <summary>
    /// Mã hồ sơ tổn thất
    /// </summary>
    public string MaHoSo { get; set; }

    /// <summary>
    /// Mã đơn vị giải quyết khiếu nại
    /// </summary>
    public string DonViGQKN { get; set; }

    /// <summary>
    /// Tên hồ sơ
    /// </summary>
    [DefaultValue(null)]
    public string TenHSBT { get; set; }

    public string DonViCD { get; set; }

    public string SoDon { get; set; }

    public string NguoiDuocBaoHiem { get; set; }

    public DateTime? NgayTonThat { get; set; }

    public decimal? SoTienYCBoiThuong { get; set; }

    public string CongTyGiamDinh { get; set; }

    [DefaultValue(null)]
    public TrangThaiTaiLieu? TrangThaiTaiLieu { get; set; }

}

public interface IKhaiBaoTonThatRepository:
     IPageListRepository<KhaiBaoTonThat>
{

}
