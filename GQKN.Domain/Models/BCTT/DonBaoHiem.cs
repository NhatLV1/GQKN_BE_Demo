namespace PVI.GQKN.Domain.Models.BCTT;

public class DonBaoHiem
{
    public string SoDon { get; private set; } // Số đơn
    public string SDBD { get; private set; } // Số đơn ban đầu
    public string LoaiSDBD { get; private set; } // Loại Số đơn ban đầu
    public DateTime NgayBatDauBH { get; private set; } // Ngày bắt đầu bảo hiểm
    public DateTime NgayKetThucBH { get; private set; } // Ngày bắt đầu bảo hiểm
}
