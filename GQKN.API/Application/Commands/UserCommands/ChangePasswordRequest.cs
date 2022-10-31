namespace PVI.GQKN.API.Application.Commands.UserCommands
{
    /// <summary>
    /// Thông tin đổi mật khẩu
    /// </summary>
    public class ChangePasswordRequest : IRequest<bool>
    {
        public string UserId { get; internal set; }
        /// <summary>
        /// Mật khẩu cũ
        /// </summary>
        public string PasswordOld { get; set; }
        /// <summary>
        /// Mật khẩu mới
        /// </summary>
        public string PasswordNew { get; set; }
        /// <summary>
        /// Nhập lại mật khẩu
        /// </summary>
        public string RewordPassword { get; set; }
        
    }
}
