using System.ComponentModel.DataAnnotations.Schema;

namespace PVI.GQKN.Domain.Models.Identity;

public enum AccountType
{
    KHACHHANG = 1,
    PIAS = 2,
    GQKN = 3,
}

public partial class ApplicationUser: IdentityUser, IAggregateRoot
{
    /// <summary>
    /// Họ tên
    /// </summary>
    [Column("fullname")]
    public string HoTen { get; set; }

    public string AnhDaiDien { get; set; }

    /// <summary>
    /// Sub-Key
    /// </summary>
    public int UserId { get; set; }


    [Column("ngay_sinh")]
    public DateTime? NgaySinh { get; set; } = null;

    [Column("dia_chi")]
    public string DiaChi { get; set; }

    //public string SoDienThoai { get; set; }
    [Column("ma_donvi")]
    public int? DonViId { get; set; }

    public DonVi DonVi { get; set; }

    public ChucDanh ChucDanh { get; set; }

    [Column("ma_chucdanh")]
    public int? ChucDanhId { get; set; }

    [Column("ma_phong")]
    public int? PhongBanId { get; set; }

    public string MaNhom { get; set; }

    public PhongBan PhongBan { get; set; }

    [Column("quan_tri")]
    public bool QuanTri { get; set; } = false;

    [Column("trang_thai_user")]
    public bool TrangThaiUser { get; set; } = true;

    [Column("loai_tai_khoan")]
    public AccountType AccountType { get; set; } = AccountType.PIAS;

    [Column("ngay_tao")]
    public DateTime NgayTao { get; set; } = DateTime.Now;

    [Column("ngay_sua")]
    public DateTime? NgaySua { get; set; }
    
    [Column("ma_dinhdanh")]
    public string MaUserPVI { get; set; }

    public void ActiveUser()
    {
        
        if (!TrangThaiUser)
        {
            TrangThaiUser = true;
            // TODO: raise event
        }
        TrangThaiUser = true;
    }

    public void Deactive()
    {
        if (!TrangThaiUser)
        {
            TrangThaiUser = false;
            // TODO: raise event
        }
        TrangThaiUser = false;
    }

}
