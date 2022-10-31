namespace PVI.GQKN.Domain.Models.HistoryLog;

public class VaiTroTrangThaiInstance
{
    public int Id { get; set; }
    public string VaiTro { get; set; }
    public string TienTrinh { get; set; }
    public string TrangThai { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } //
    public int? VaiTroTrangThaiID { get; set; }
    public int? HoSoId { get; set; } // index
}
