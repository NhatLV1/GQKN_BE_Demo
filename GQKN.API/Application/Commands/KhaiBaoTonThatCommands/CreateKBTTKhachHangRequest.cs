namespace PVI.GQKN.API.Application.Commands.KhaiBaoTonThatCommands;

public class CreateKBTTKhachHangRequest: IRequest<KhaiBaoTonThat>
{
    /// <summary>
    /// Họ và tên người được bảo hiểm.
    /// </summary>
    [Required]
    public string HoTen { get; set; }
  
    /// <summary>
    /// Địa chỉ người được bảo hiểm.
    /// </summary>
    public string DiaChi { get; set; }

    /// <summary>
    /// Họ tên người liên hệ.
    /// </summary>
    [Required]
    public string NguoiLienHe { get; set; }

    /// <summary>
    /// Đối tượng bị tổn thất
    /// </summary>
    [Required]
    public string DoiTuongDuocBaoHiem { get; set; }

    /// <summary>
    /// Địa chỉ email người liên hệ.
    /// </summary>
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    /// <summary>
    /// Số điện thoại người liên hệ.
    /// </summary>
    [DataType(DataType.PhoneNumber)]
    [Required]
    public string SoDienThoai { get; set; }

    /// <summary>
    /// Số hợp đồng
    /// </summary>
    public string SoHopDong { get; set; }

    /// <summary>
    /// Số đơn bảo hiểm.
    /// </summary>
    [Required]
    public string SoDonBaoHiem { get; set; }

    /// <summary>
    /// Số đơn bổ sung.
    /// </summary>
    public string SoDonSDBS { get; set; }

    /// <summary>
    /// Ngày bắt đầu bảo hiểm.
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime NgayBatDauBH { get; set; }

    /// <summary>
    /// Ngày kết thúc bảo hiểm.
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime NgayKetThucBH { get; set; }

    /// <summary>
    /// Mã đơn vị cấp đơn.
    /// </summary>
    [Required]
    public int? DonViCapDonId { get; set; }  
   
    /// <summary>
    /// Đối tượng bị tổn thất
    /// </summary>
    [Required]
    public string DoiTuongBiTonThat { get; set; }

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
    [DefaultValue(null)]
    public decimal? UocLuongTonThat { get; set; }


    /// <summary>
    /// Mã loại tiền (của ước lượng tổn thất).
    /// </summary>
    [DefaultValue(null)]
    public int? TyGiaId { get; set; }

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
    public string DeXuat { get; set; }
}
