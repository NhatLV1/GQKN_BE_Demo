using System.ComponentModel.DataAnnotations.Schema;

namespace PVI.GQKN.Infrastructure.Idempotency;

public class BieuMau: Entity, IAggregateRoot
{
    public string TenBieuMau { get; set; }

    public string NoiDung { get; set; }

    [NotMapped]
    public DateTime? NgayTruyXuat { get; set; }
    
    public string Location => this.TenBieuMau;
}
