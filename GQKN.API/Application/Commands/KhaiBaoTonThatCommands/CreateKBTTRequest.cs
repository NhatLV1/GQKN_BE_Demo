namespace PVI.GQKN.API.Application.Commands.KhaiBaoTonThatCommands;

/// <summary>
/// Khai báo tổn thất của khách hàng hoặc môi giới 
/// </summary>
public class CreateKBTTKRequest
{
    /// <summary>
    /// Họ và tên người được bảo hiểm.
    /// </summary>
    [Required]
    public string NguoiDuocBaoHiem_HoTen { get; set; }
  
    /// <summary>
    /// Địa chỉ người được bảo hiểm.
    /// </summary>
    public string NguoiDuocBaoHiem_DiaChi { get; set; }

    /// <summary>
    /// Họ tên người liên hệ.
    /// </summary>
    [Required]
    public string NguoiLienHe_HoTen { get; set; }

    /// <summary>
    /// Địa chỉ email người liên hệ.
    /// </summary>
    [DataType(DataType.EmailAddress)]
    public string NguoiLienHe_Email { get; set; }

    /// <summary>
    /// Số điện thoại người liên hệ.
    /// </summary>
    [DataType(DataType.PhoneNumber)]
    [Required]
    public string NguoiLienHe_SoDienThoai { get; set; }

    /// <summary>
    /// Số hợp đồng
    /// </summary>
    public string SoHopDong { get; set; }

    /// <summary>
    /// Số đơn bảo hiểm.
    /// </summary>
    [Required]
    public string DonBaoHiemSoDon { get; set; }

    /// <summary>
    /// Số đơn bổ sung.
    /// </summary>
    public string DonBaoHiemSDBS { get; set; }

    /// <summary>
    /// Ngày bắt đầu bảo hiểm.
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime DonBaoHiemNgayBatDauBH { get; set; }

    /// <summary>
    /// Ngày kết thúc bảo hiểm.
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime DonBaoHiemNgayKetThucBH { get; set; }

    /// <summary>
    /// Mã đơn vị cấp đơn.
    /// </summary>
    [Required]
    public int? DonViCapDonId { get; set; }

    /// <summary>
    /// Đối tượng bị tổn thất
    /// </summary>
    [Required]
    public string DoiTuongTonThat { get; set; }

    /// <summary>
    /// Thời gian tổn thất
    /// </summary>
    [DataType(DataType.Date)]
    [Required]
    public DateTime ThoiGianTonThat { get; set; }

    /// <summary>
    /// Địa điểm tổn thất.
    /// </summary>
    [Required]
    public string DiaDiemTonThat { get; set; }

    /// <summary>
    /// Ước lượng tổn thất.
    /// </summary>
    public decimal? UocLuongTonThat { get; set; }

    /// <summary>
    /// Đơn vị tiền tệ (của ước lượng tổn thất).
    /// </summary>
    public string DonViTienTe { get; set; }

    /// <summary>
    /// Nguyên nhân sơ bộ.
    /// </summary>
    [Required]
    public string NguyenNhanSoBo { get; set; }

    /// <summary>
    /// Phương án khắc phục thiệt hại.
    /// </summary>
    public string PhuongAnKhacPhuc { get; set; }

    /// <summary>
    /// Thông tin khác.
    /// </summary>
    public string ThongTinKhac { get; set; }

    /// <summary>
    /// Đề xuất/đề nghị.
    /// </summary>
    public string DeXuatDeNghi { get; set; }
}
