namespace PVI.GQKN.API.Application.Commands;

public class CreateEntityCommand<E, R>: IRequest<E>
    where R : IRepository<E> 
    where E: class, IAggregateRoot
{
}
