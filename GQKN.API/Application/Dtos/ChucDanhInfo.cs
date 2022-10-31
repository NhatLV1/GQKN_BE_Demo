namespace PVI.GQKN.API.Application.Dtos;

public class ChucDanhInfo
{
    public int Id { get; private set; }
    public string Guid { get; private set; }
    public string MaChucVu { get; private set; }
    public string TenChucVu { get; private set; }

    public ChucDanhInfo(int id, string guid, string maChucVu, string tenChucVu)
    {
        Id = id;
        Guid = guid;
        MaChucVu = maChucVu;
        TenChucVu = tenChucVu;
    }
}
