namespace PVI.GQKN.API.Application.Dtos;

public class PhongBanDto: IEntity
{
    public int Id { get; private set; }

    public string Guid { get; private set; }

    public string TenPhongBan { get; private set; }

    public LoaiPhongBan LoaiPhongBan { get; private set; }

    public int? PhongBanChaId { get; private set; }

    public DateTime NgayTao { get; private set; }

    public DateTime? NgaySua { get; private set; }

    public int? DonViId { get; private set; }

    public PhongBanDto(int id, string guid, string tenPhongBan, 
        LoaiPhongBan loaiPhongBan, int? phongBanChaId, DateTime ngayTao, DateTime? ngaySua,
        int? donViId
        )
    {
        Id = id;
        Guid = guid;
        TenPhongBan = tenPhongBan;
        LoaiPhongBan = loaiPhongBan;
        PhongBanChaId = phongBanChaId;
        NgayTao = ngayTao;
        NgaySua = ngaySua;
        DonViId = donViId;
    }
}
