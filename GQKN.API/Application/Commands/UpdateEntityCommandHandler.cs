namespace PVI.GQKN.API.Application.Commands;

public class UpdateEntityCommandHandler<E, R> : IRequestHandler<UpdateEntityCommand<E, R>, E>
    where R : IRepository<E>
    where E : class, IAggregateRoot
{
    private readonly ILogger<UpdateEntityCommandHandler<E, R>> logger;
    private readonly IMapper mapper;
    private IRepository<E> repository;

    public UpdateEntityCommandHandler(
       IRepository<E> repository,
       ILogger<UpdateEntityCommandHandler<E, R>> logger,
       IMapper mapper)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<E> Handle(UpdateEntityCommand<E, R> request, CancellationToken cancellationToken)
    {
        var template = this.repository.GetByID(request.Id);
        if (template != null)
        {
            template = this.mapper.Map(request, template);
            this.repository.Update(template);
            try
            {
                if (await this.repository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                {
                    return template;
                }
            }
            // TODO: fix exception catching
            catch (DbUpdateException ex)
            {
                logger.LogError(ex.Message);
            }
        }
        return null;
    }

}
