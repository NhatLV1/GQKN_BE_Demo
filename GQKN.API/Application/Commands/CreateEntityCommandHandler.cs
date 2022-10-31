namespace PVI.GQKN.API.Application.Commands;

public class CreateEntityCommandHandler<E, R> : IRequestHandler<CreateEntityCommand<E, R>, E>
    where R : IRepository<E>
    where E : class, IAggregateRoot
{
    private readonly R repository;
    private readonly IMapper mapper;
    private readonly ILogger<CreateEntityCommandHandler<E, R>> logger;

    public CreateEntityCommandHandler(
        R repository,
        IMapper mapper, 
        ILogger<CreateEntityCommandHandler<E, R>> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<E> Handle(CreateEntityCommand<E, R> request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<E>(request);
        var result = repository.Insert(entity);
        try
        {
            if (await repository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
            {
                return result;
            }
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex.Message);
        }

        return null;
    }
}
