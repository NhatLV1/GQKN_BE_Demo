using AutoMapper;
using Microsoft.Azure.Amqp.Encoding;
using PVI.GQKN.API.Services.Auth;

namespace PVI.GQKN.API.Application.Commands.DonViCommands;

public class UpdateDonViRequest: IRequest<DonVi>
{
    public string Id { get; internal set; }
    /// <summary>
    /// Mã đơn vị
    /// </summary>
    [MaxLength(10)]
    [Required]
    public string MaDonVi { get; set; }

    /// <summary>
    /// Tên đơn vị
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string TenDonVi { get; set; }

    /// <summary>
    /// Mã tỉnh. (field id của table Tỉnh thành)
    /// </summary>
    //[Required]
    [MaxLength(10)]
    public string MaTinh { get; set; }
    
    /// <summary>
    /// Danh sách các scope (VD: ADMIN, KBTT, BCTT, ..)
    /// </summary>
    public List<string> Scopes { get; set; }
}

public class UpdateDonViRequestHandler : IRequestHandler<UpdateDonViRequest, DonVi>
{
    private readonly ILogger<UpdateDonViRequest> logger;
    private readonly IMapper mapper;
    private readonly IAuthService authService;
    private readonly IDonViRepository donViRepository;

    public UpdateDonViRequestHandler(
        ILogger<UpdateDonViRequest> logger,
        IMapper mapper,
        IAuthService authService,
        IDonViRepository donViRepository)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.authService = authService;
        this.donViRepository = donViRepository;
    }

    public async Task<DonVi> Handle(UpdateDonViRequest request, CancellationToken cancellationToken)
    {
        var donVi = await this.donViRepository.GetByGuidAsync(request.Id);

        if (donVi != null)
        {
            this.mapper.Map(request, donVi);
            donVi.ClearScopes();
            if (request.Scopes != null)
            {
                var aclScopes = authService.GetScopes();
                var selectedScopes = aclScopes.Where(e => request.Scopes.Contains(e.Id));
                var scopes = mapper.Map<IEnumerable<Scope>>(selectedScopes);
                donVi.AddScope(scopes);
            }

            this.donViRepository.Update(donVi);
        }

        return donVi;
        
    }
}
