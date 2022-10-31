namespace PVI.GQKN.API.Application.Commands.UserCommands
{

    /// <summary>
    /// Quên mật khẩu request
    /// </summary>
    public class ForgotPasswordRequest : IRequest<string>
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }
        
        
    }
}
