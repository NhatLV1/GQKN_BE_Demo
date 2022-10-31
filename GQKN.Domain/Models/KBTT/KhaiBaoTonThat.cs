using System.ComponentModel.DataAnnotations.Schema;

namespace PVI.GQKN.Domain.Models.KBTT;

public enum TrangThaiTaiLieu
{
    ConThieu = 0,
    DayDu = 1,
}

public  class KhaiBaoTonThat : Entity, IAggregateRoot
{
   
    [Column("ma_dinhdanh")]
    public string MaDinhDanh { get; set; }

    [Column("ho_ten")]
    public string HoTen { get; set; }

    [Column("nguoi_lhe")]
    public string NguoiLienHe { get; set; }

    [Column("dia_chi")]
    public string DiaChi { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("so_dthoai")]
    public string SoDienThoai { get; set; }

    [Column("so_hdong")]
    public string SoHopDong { get; set; }

    [Column("so_don_bh")]
    public string SoDonBaoHiem { get; set; }

    [Column("so_don_sdbs")]
    public string SoDonSDBD { get; set; }

    [Column("dtuong_bi_tt")]
    public string DoiTuongBiTonThat { get; set; }

    [Column("ngay_bdau_bh")]
    public DateTime? NgayBatDauBH { get; set; }

    [Column("ngay_kthuc_bh")]
    public DateTime? NgayKetThucBH { get; set; }

    [Column("dtuong_duoc_bh")]
    public string DoiTuongDuocBaoHiem { get; set; }

    [Column("donvi_gqkn")]
    public int? GQKNId { get; set; }

    public DonVi GQKN { get; private set; }

    [Column("donvi_cdon")]
    public int? DonViCapDonId { get; set; }

    public DonVi DonViCapDon { get; private set; }

    [Column("tgian_tt")]
    public DateTime ThoiGianTonThat { get; set; }

    [Column("ma_tinhtp")]
    public int? MaTinhId { get; set; }

    [Column("dia_diem_tt")]
    public string DiaDiemTonThat { get; set; }

    [Column("uoc_luong_tt")]
    public decimal? UocLuongTonThat { get; set; }

    [Column("ma_tygia")]
    public int? TyGiaId { get; set; }

    [Column("nnhan_so_bo")]
    public string NguyenNhanSoBo { get; set; }

    [Column("pa_kphuc")]
    public string PhuongAnKhacPhuc { get; set; }

    [Column("ttin_khac")]
    public string ThongTinKhac { get; set; }

    [Column("ma_httn")]
    public int? HinhThucId { get; set; }

    [Column("tgian_tnhan")]
    public DateTime? ThoigianTiepNhan { get; set; }

    [Column("ma_user_nhan")]
    public int? UserNhanId { get; set; }

    [Column("de_xuat")]
    public string DeXuat { get; set; }

    [Column("tthai_kbtt")]
    public bool? TrangThai { get; set; } = true;

    
    public TrangThaiTaiLieu TrangThaiTaiLieu { get; set; } = TrangThaiTaiLieu.ConThieu;

    public string TaiLieuConThieu { get; set; }

    public KhaiBaoTonThat()
    {

    }
}
