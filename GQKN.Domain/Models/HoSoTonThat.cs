using PVI.GQKN.Domain.Seedwork;

namespace PVI.GQKN.Domain.Models;

public partial class DonVi
{
    public List<HoSoTonThat> HoSoTonThats { get; set; }
}

public partial class HoSoTonThat: 
    Entity, IAggregateRoot
{
    public LienHe NguoiDuocBaoHiem { get; set; }

    public LienHe NguoiLienHe { get; set; }

    public string SoHopDong { get; set; }

    public DonBaoHiem DonBaoHiem { get; set; }

    public int? DonViCapDonId { get; set; }

    public DonVi DonViCapDon { get; private set; }

    public string DoiTuongTonThat { get; set; }

    public DateTime ThoiGianTonThat { get; set; }

    public string DiaDiemTonThat { get; set; }

    public decimal? UocLuongTonThat { get; set; }

    public string DonViTienTe { get; set; }

    public string NguyenNhanSoBo { get; set; }

    public string PhuongAnKhacPhuc { get; set; }

    public string ThongTinKhac { get; set; }

    public string DeXuatDeNghi { get; set; }

    public bool TrangThaiHoSo { get; set; } = true;

    public TrangThaiTaiLieu TrangThaiTaiLieu { get; set; } = TrangThaiTaiLieu.ConThieu;

    public string TaiLieuConThieu { get; set; }

    public int? NguoiTaoId { get; set; }
}
