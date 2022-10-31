namespace PVI.GQKN.API.Application.Commands.KhaiBaoTonThatCommands;

public class CreateKhaiBaoTonThatRequest: IRequest<KhaiBaoTonThat>
{
    //public LienHe NguoiDuocBaoHiem { get; set; }

    //public LienHe NguoiLienHe { get; set; }
  

    public string SoHopDong { get; set; }

    public DonBaoHiem DonBaoHiem { get; set; }

    public int? DonViCapDonId { get; set; }

    public string DoiTuongTonThat { get; set; }

    public DateTime ThoiGianTonThat { get; set; }

    public string DiaDiemTonThat { get; set; }

    public decimal UocLuongTonThat { get; set; }

    public string DonViTienTe { get; set; }

    public string NguyenNhanSoBo { get; set; }

    public string PhuongAnKhacPhuc { get; set; }

    public string ThongTinKhac { get; set; }

    public string DeXuatDeNghi { get; set; }

}
