namespace PVI.GQKN.Domain.Models;

public class DonBaoHiem
{
    /// <summary>
    /// Số đơn
    /// </summary>
    public string SoDon { get; set; }
    
    public string SDBD { get; set; }
    
    public DateTime NgayBatDauBH { get; set; }

    public DateTime NgayKetThucBH { get; set; }
}
