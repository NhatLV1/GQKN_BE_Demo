namespace PVI.GQKN.API.Application.Commands;

public class DeleteEntityCommand<E, R>: IRequest<bool>
    where R : IRepository<E>
    where E : class, IAggregateRoot
{
    public int Id { get; set; }
}
