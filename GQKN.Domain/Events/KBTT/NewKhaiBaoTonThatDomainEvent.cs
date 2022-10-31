namespace PVI.GQKN.Domain.Events.KBTT;

public class NewKhaiBaoTonThatDomainEvent : INotification
{
    public KhaiBaoTonThat KhaiBaoTonThat { get; }

    public NewKhaiBaoTonThatDomainEvent(KhaiBaoTonThat khaiBaoTonThat)
    {
        KhaiBaoTonThat = khaiBaoTonThat;
    }
}