namespace PVI.GQKN.Domain.Models;

public class DoiTac: Entity, IAggregateRoot
{
    public string TenThuongMai { get; set; }
    public string TenLienHe { get; set; }
    public string SoDienThoai { get; set; }
    public string Email { get; set; }
    public string DiaChi { get; set; }

}
