
namespace PVI.GQKN.API.Application.Commands.UserCommands;

public class UpdateUserRequest: IRequest<ApplicationUser>
{
    /// <summary>
    /// User id
    /// </summary>
    public string __UserId { get; internal set; }

    /// <summary>
    /// Danh sách tên vai trò
    /// </summary>
    [Required]
    public string[] VaiTro { get; set; }
   
    /// <summary>
    /// Họ và tên
    /// </summary>
    [Required]
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
    /// Email
    /// </summary>
    [Required]
    public string Email { get; set; }
    /// <summary>
    /// Số điện thoại
    /// </summary>
    [Required]
    public string SoDienThoai { get; set; }

}
