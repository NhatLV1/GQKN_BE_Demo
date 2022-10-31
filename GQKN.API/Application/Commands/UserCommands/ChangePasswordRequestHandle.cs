namespace PVI.GQKN.API.Application.Commands.UserCommands
{
    public class ChangePasswordRequestHandle : IRequestHandler<ChangePasswordRequest, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthPVI _pviAuth;
        private readonly IDonViRepository donViRepository;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IPhongBanRepository phongBanRepository;
        private readonly IMapper _mapper;
        public ChangePasswordRequestHandle(
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
        public async Task<bool> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new GQKNDomainException("Find user not found");
                }
                if (request.PasswordNew.Equals(request.PasswordOld))
                {
                    throw new GQKNDomainException("Password new the same password old");
                }
                if (!request.PasswordNew.Equals(request.RewordPassword))
                {
                    throw new GQKNDomainException("Password new not same password retype");
                }
                var checkPass = await _userManager.CheckPasswordAsync(user, request.PasswordOld);
                if (checkPass)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, request.PasswordOld, request.PasswordNew);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                    else
                    {
                        IdentityError error = result.Errors.First();
                        throw new GQKNDomainException(error.Code, error.Description);
                    }
                }
                else
                {
                    throw new GQKNDomainException("Password old invalid");
                }
            }
            catch (HttpRequestException e)
            {
                throw e;
            }

        }
    }
}
