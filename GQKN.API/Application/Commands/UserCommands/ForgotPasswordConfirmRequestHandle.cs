namespace PVI.GQKN.API.Application.Commands.UserCommands
{
    public class ForgotPasswordConfirmRequestHandle : IRequestHandler<ForgotPasswordConfirmRequest, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthPVI _authPVI;
        public ForgotPasswordConfirmRequestHandle(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IAuthPVI authPVI,
        IDonViRepository donViRepository,
        IPhongBanRepository phongBanRepository,
        IMapper mapper)
        {
            this._authPVI = authPVI;
            this._userManager = userManager;
            this._mapper = mapper;
        }
        public async Task<bool> Handle(ForgotPasswordConfirmRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new GQKNDomainException("Find user not found");
                }
                // check valid otp
                if (request.Code.Equals("123456789"))
                {
                    var checkPass = await _userManager.CheckPasswordAsync(user, request.Password);
                    if (!checkPass)
                    {

                        string passwordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
                        user.PasswordHash = passwordHash;
                        var result = await _userManager.UpdateAsync(user);

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
                        throw new GQKNDomainException("Password new same password");
                    }
                }
                else
                {
                    throw new GQKNDomainException("Otp invalid");
                }

            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }
    }
}
