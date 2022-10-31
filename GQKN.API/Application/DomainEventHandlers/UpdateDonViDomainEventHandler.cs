using PVI.GQKN.Domain.Events;

namespace PVI.GQKN.API.Application.DomainEventHandlers;
public class UpdateDonViDomainEventHandler : INotificationHandler<UpdateDonViDomainEvent>
{
    public Task Handle(UpdateDonViDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Hello Log 1");
        return Task.CompletedTask;
    }
}
