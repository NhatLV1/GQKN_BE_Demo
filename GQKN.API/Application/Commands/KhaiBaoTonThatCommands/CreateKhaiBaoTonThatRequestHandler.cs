namespace PVI.GQKN.API.Application.Commands.KhaiBaoTonThatCommands
{
    public class CreateKhaiBaoTonThatRequestHandler :
        IRequestHandler<CreateKhaiBaoTonThatRequest, KhaiBaoTonThat>
    {
        private readonly IKhaiBaoTonThatRepository repository;
        private readonly IMapper mapper;

        public CreateKhaiBaoTonThatRequestHandler(
            IKhaiBaoTonThatRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<KhaiBaoTonThat> Handle(CreateKhaiBaoTonThatRequest request, CancellationToken cancellationToken)
        {
            var item = mapper.Map<KhaiBaoTonThat>(request);
            item.AddDomainEvent(new NewKhaiBaoTonThatDomainEvent(item));
            
            this.repository.Insert(item);

            await this.repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return item;
        }
    }
}
