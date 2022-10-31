namespace PVI.GQKN.API.Application.Commands.ChucDanhCommands
{
    public class CreateChucDanhCommandHanlers : IRequestHandler<CreateChucDanhCommand , ChucDanh>
    {
        private readonly IChucDanhRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateChucDanhCommandHanlers> logger;

        public CreateChucDanhCommandHanlers(IMapper mapper,
            ILogger<CreateChucDanhCommandHanlers> logger,
            IChucDanhRepository chucDanhRepository)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.repository = chucDanhRepository;
        }

        public async Task<ChucDanh> Handle(CreateChucDanhCommand request, CancellationToken cancellationToken)
        {
            var item = new ChucDanh(request.MaChucVu, request.TenChucVu);

            try
            {
                this.repository.Insert(item);
                await repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                logger.LogError(e.Message);
                item = null;
            }

            return item;
        }
    }
}
