

namespace PVI.GQKN.API.Services;

public interface IAuthPVI
{
    public const string LOGIN_TYPE_QLCD = "QLCD";
    public const string LOGIN_TYPE_MAIL = "MAIL_PVI";

    Task<LoginResultPVIDto> Login(string username, string password, string type);

    Task<VerifyTokenResultPVIDto> VerifyToken(string token);

    Task<UpdateUserPVIResult> UpdateUser(UpdatePVIUserRequest content);

    Task<DeleteUserPVIResult> DeleteUser(string userId);

    Task<RegisterUserPVIResult> RegisterUser(RegisterPVIUserRequest request);
}

public class UpdatePVIUserRequest
{
    public string UserId { get; set; }
    public string Fullname { get; set; }
    public string Password { get; set; }
}

public class RegisterPVIUserRequest
{
    public string Username { get; set; }
    public string  Fullname { get; set; }
    public int Status { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string MaDonVi { get; set; }
    public string MaPhongBan { get; set; }

}

public class RegisterUserPVI_Content
{
    public string ten_user { get; set; }
    public string full_name { get; set; }
    public int trang_thai { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string ma_donvi { get; set; }
    public string ma_phong { get; set; }
    public string CpId { get; set; }
    public string Sign { get; set; }
}

public class RegisterUserPVIResult
{
    public string Status { get; set; }
    public string Message { get; set; }
    public string ma_user { get; set; }

    public bool Successed => Status == "00";
}

public class UpdateUserPVI_Content
{
    public string ma_user { get; set; }
    public string full_name { get; set; }
    public string password { get; set; }
    public string CpId { get; set; }
    public string Sign { get; set; }
}

public class UpdateUserPVIResult
{
    public string Status { get; set; }
    public string Message { get; set; }
    public bool Successed => Status == "00";

}

public class DeleteUserPVI_Content
{
    public string ma_user { get; set; }
    public string CpId { get; set; }
    public string Sign { get; set; }
}

public class DeleteUserPVIResult
{
    public string Status { get; set; }
    public string Message { get; set; }

    public bool Successed => Status == "00";

}
