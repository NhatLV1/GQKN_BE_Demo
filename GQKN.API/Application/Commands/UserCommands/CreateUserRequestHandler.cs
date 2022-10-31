using Microsoft.AspNetCore.Identity;

namespace PVI.GQKN.API.Application.Commands.UserCommands;

public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthPVI _pviAuth;
    private readonly IDonViRepository donViRepository;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly IPhongBanRepository phongBanRepository;
    private readonly IMapper _mapper;

    public CreateUserRequestHandler(
        UserManager<ApplicationUser> userManager, 
        RoleManager<ApplicationRole> roleManager,
        IAuthPVI authPVI,
        IDonViRepository donViRepository,
        IPhongBanRepository phongBanRepository,
        IMapper mapper)
    {
        this._userManager = userManager;
        this._pviAuth = authPVI;
        this.donViRepository = donViRepository;
        this.roleManager = roleManager;
        this.phongBanRepository = phongBanRepository;
        this._mapper = mapper;
    }

    public async Task<ApplicationUser> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var pviRequest = new RegisterPVIUserRequest()
            {
                Username = request.Username,
                Password = request.Password,
                Fullname = request.HoTen,
                Status = 1,
                Email = request.Email,
                MaDonVi = donViRepository.GetByID(request.DonViId.Value)?.MaDonVi,
                MaPhongBan = phongBanRepository.GetByID(request.PhongbanId.Value)?.MaPhongBan,
            };

            var result = await _pviAuth.RegisterUser(pviRequest);
            if (result.Successed)
            {
                var user = await _userManager.FindByNameAsync(request.Username);

                if (user == null)
                {
                    user = this._mapper.Map<ApplicationUser>(request);
                    user.MaUserPVI = request.MaUserPVI;
                    var localResult = await _userManager.CreateAsync(user);
                    if(!localResult.Succeeded)
                    {
                        IdentityError error = localResult.Errors.First();
                        throw new GQKNDomainException(error.Code, error.Description);
                    }

                }

                await roleManager.UpdateUserRoles(_userManager, request.VaiTro, user);
                

                return user;
            }
            else
            {
                throw new GQKNDomainException(result.Message);
            }
            
        }
        catch (HttpRequestException e)
        {
            throw e;
        }

    }
}
