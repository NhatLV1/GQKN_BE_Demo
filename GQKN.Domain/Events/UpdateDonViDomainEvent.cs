using PVI.GQKN.Domain.Models;

namespace PVI.GQKN.Domain.Events;

public class UpdateDonViDomainEvent : INotification
{
    public PhongBan DonVi { get; }

    public UpdateDonViDomainEvent(PhongBan donvi)
    {
        DonVi = donvi;
    }
}
