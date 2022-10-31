namespace PVI.GQKN.API.Application.Dtos;

public class KhaiBaoTonThatKhachHangDto
{
    /// <summary>
    /// Mã hồ sơ
    /// </summary>
    public string MaDinhDanh { get; set; }

    /// <summary>
    /// Đơn vị GQKN
    /// </summary>
    public string DVGQTenDonVi { get; set; }

    /// <summary>
    /// Tên HSBT
    /// </summary>
    public string TenHSBT { get; set; }

    /// <summary>
    /// Tên đơn vị cấp đơn
    /// </summary>
    public string DonViCapDonTenDonVi { get; private set; }

    /// <summary>
    /// Số đơn bảo hiểm
    /// </summary>
    public string SoDonBaoHiem { get; set; }

    /// <summary>
    /// Người được BH
    /// </summary>
    public string HoTen { get; set; }

    /// <summary>
    /// Ngày tổn thất
    /// </summary>
    public DateTime ThoiGianTonThat { get; set; }

    /// <summary>
    /// Số tiền yêu cầu bồi thường
    /// </summary>
    public decimal? UocLuongTonThat { get; set; }

    /// <summary>
    /// Công ty giám định
    /// </summary>
    public string CongTyGiamDinh { get; set; }

    /// <summary>
    /// Trạng thái tài liệu
    /// </summary>
    public TrangThaiTaiLieu TrangThaiTaiLieu { get; set; }

    /// <summary>
    /// Tài liệu còn thiếu
    /// </summary>
    public string TaiLieuConThieu { get; set; }
}
