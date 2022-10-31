namespace PVI.GQKN.Domain.Models;

public class ChucDanh : Entity, IAggregateRoot
{
    public string MaChucVu { get; set; }

    public string TenChucVu { get; set; }

    public ChucDanh()
    {

    }
    public ChucDanh(string maChucVu, string tenChucVu)
    {
         MaChucVu = maChucVu;
        TenChucVu = tenChucVu;
    }

}