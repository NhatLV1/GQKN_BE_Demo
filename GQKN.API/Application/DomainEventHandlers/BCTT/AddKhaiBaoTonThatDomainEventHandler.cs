namespace PVI.GQKN.API.Application.DomainEventHandlers.BCTT;

public class AddKhaiBaoTonThatDomainEventHandler :
    INotificationHandler<NewKhaiBaoTonThatDomainEvent>
{
    public Task Handle(NewKhaiBaoTonThatDomainEvent notification, CancellationToken cancellationToken)
    {
        //Debug.WriteLine("hello");
        throw new NotImplementedException();
    }
}
