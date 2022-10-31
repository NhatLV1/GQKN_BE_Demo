namespace PVI.GQKN.API.Application.Commands.UserCommands
{
    public class ForgotPasswordRequestHandle : IRequestHandler<ForgotPasswordRequest, string>
    {
        public UserManager<ApplicationUser> _userManager { get; set; }
        public ForgotPasswordRequestHandle(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new GQKNDomainException("Find user not found");
                }
                string code = "123456789";
                // save code table or send email confirm otp
                return code;
                

            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }
    }
}
