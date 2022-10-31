namespace PVI.GQKN.Domain.Events.KBTT;

public class UpdateKhaiBaoTonThatDomainEvent
{
    public KhaiBaoTonThat KhaiBaoTonThat { get; }
    public UpdateKhaiBaoTonThatDomainEvent(KhaiBaoTonThat khaiBaoTonThat)
    {
        KhaiBaoTonThat = khaiBaoTonThat;
    }
}
