
namespace PVI.GQKN.Domain.Models;

public enum LoaiPhongBan
{
    Ban = 1,
    Phong = 2,
}

public class PhongBan: Entity, IAggregateRoot
{
    public string TenPhongBan { get;  set; }

    public int? PhongBanChaId { get; set; }

    public PhongBan PhongBanCha { get;  set; }

    public LoaiPhongBan LoaiPhongBan { get; set; } = LoaiPhongBan.Phong;

    public int? DonViId { get; set; }

    public DonVi DonVi { get; set; }

    [JsonIgnore]
    public List<KhaiBaoTonThat> KhaiBaoTonThats { get; set; }
    public string MaPhongBan { get; set; }

    public PhongBan()
    {

    }
}
