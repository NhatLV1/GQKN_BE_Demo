namespace PVI.GQKN.API.Application.Commands.UserCommands;

/// <summary>
/// Thông tin tài khoản
/// </summary>
public class CreateUserRequest: IRequest<ApplicationUser>
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// Họ và tên
    /// </summary>
    public string HoTen { get; set; }

    /// <summary>
    /// Mã nhân viên
    /// </summary>
    [Required]
    public string MaUserPVI { get; set; }
    /// <summary>
    /// Ngày sinh
    /// </summary>
    [Required]
    public DateTime NgaySinh { get; set; }
    /// <summary>
    /// Mã đơn vị quản lý
    /// </summary>
    [Required]
    public int? DonViId { get; set; }
    /// <summary>
    /// Mã phòng ban quản lý
    /// </summary>
    [Required]
    public int? PhongbanId { get; set; }
    /// <summary>
    /// Mã chức danh
    /// </summary>
    [Required]
    public int? ChucDanhId { get; set; }
    /// <summary>
    /// Địa chỉ
    /// </summary>
    [Required]
    public string DiaChi { get; set; }

    /// <summary>
    /// Số điện thoại
    /// </summary>
    [Required]
    public string SoDienThoai { get; set; }
    public string[] VaiTro { get;  set; }
}
