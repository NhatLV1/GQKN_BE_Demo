using PVI.GQKN.Domain.Events;

namespace PVI.GQKN.API.Application.DomainEventHandlers;
public class UpdateDonViDomainEventHandler2 : INotificationHandler<UpdateDonViDomainEvent>
{
    public Task Handle(UpdateDonViDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Hello Log 2");
        return Task.CompletedTask;
    }
}
