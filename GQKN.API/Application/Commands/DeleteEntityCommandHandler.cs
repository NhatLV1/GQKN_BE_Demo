namespace PVI.GQKN.API.Application.Commands;

public class DeleteEntityCommandHandler<E, R> : IRequestHandler<DeleteEntityCommand<E, R>, bool>
    where R : IRepository<E>
    where E : class, IAggregateRoot
{
    private readonly IRepository<E> repository;
    private readonly ILogger<DeleteEntityCommandHandler<E, R>> logger;

    public DeleteEntityCommandHandler(
        IRepository<E> repository,
        ILogger<DeleteEntityCommandHandler<E, R>> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<bool> Handle(DeleteEntityCommand<E, R> request, CancellationToken cancellationToken)
    {
        if (this.repository.Delete(request.Id))
        {
            try
            {
                if (await this.repository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                {
                    return true;
                }
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex.Message);
            }
        }
        return false;
    }
}
