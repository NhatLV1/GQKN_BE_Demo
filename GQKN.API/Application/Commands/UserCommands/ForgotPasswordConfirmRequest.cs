namespace PVI.GQKN.API.Application.Commands.UserCommands
{
    public class ForgotPasswordConfirmRequest : IRequest<bool>
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Password new
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Renew password
        /// </summary>
        public string RetypePassword { get; set; }

        /// <summary>
        /// Mã xác thực quên mật khẩu
        /// </summary>
        public string Code { get; set; }
    }
}
