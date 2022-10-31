using System.ComponentModel.DataAnnotations.Schema;

namespace PVI.GQKN.API.Application.Dtos;

public class KhaiBaoTonThatKhachHangInfo
{
    public int Id { get; set; }

    public string Guid { get; set; }

    /// <summary>
    /// Mã hồ sơ
    /// </summary>
    public string NguoiLienHe { get; set; }

    /// <summary>
    /// Mã hồ sơ
    /// </summary>
    public string DoiTuongDuocBaoHiem { get; set; }
    public int? GQKNId { get; set; }

    public string Email { get; set; }

    public string SoDienThoai { get; set; }

    public string SoDonSDBD { get; set; }

    public DateTime? NgayBatDauBH { get; set; }

    public DateTime? NgayKetThucBH { get; set; }

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
    /// Mã đơn vị cấp đơn
    /// </summary>
    public int? DonViCapDonId { get; private set; }
    public string DiaDiemTonThat { get; set; }
    public string NguyenNhanSoBo { get; set; }
    public string PhuongAnKhacPhuc { get; set; }
    public string ThongTinKhac { get; set; }
    public DateTime? ThoigianTiepNhan { get; set; }
    public int? HinhThucId { get; set; }
    public int? UserNhanId { get; set; }
    public string DeXuat { get; set; }

    public int? MaTinhId { get; set; }


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
