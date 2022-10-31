namespace PVI.GQKN.Domain.Models.BCTT;

public class BaoCaoTonThat: Entity, IAggregateRoot
{
    public DonBaoHiem DonBaoHiem { get; private set; }
    
    private List<DongBaoHiem> _dongBaoHiems;
    public IEnumerable<DongBaoHiem> DongBaoHiems => _dongBaoHiems.AsReadOnly();

    private List<TaiBaoHiem> _taiBaoHiems;
    public IEnumerable<TaiBaoHiem> TaiBaoHiems => _taiBaoHiems.AsReadOnly();

    private List<HangMucBaoHiem> _hangMucBaoHiems;
    public IEnumerable<HangMucBaoHiem> HangMucBaoHiems => _hangMucBaoHiems.AsReadOnly();

    
}
