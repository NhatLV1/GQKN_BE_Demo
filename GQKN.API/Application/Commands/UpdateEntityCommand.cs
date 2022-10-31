namespace PVI.GQKN.API.Application.Commands;

public class UpdateEntityCommand<E, R>: IRequest<E>
    where R : IRepository<E>
    where E : class, IAggregateRoot
{
    public int Id { get; set; }
}
