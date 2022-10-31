namespace PVI.GQKN.API.Services.ModelDTOs;

public enum LoginStatusPVI
{ 
    Success = 0,
    Error = -404,
    Exception = -1,
    DataError = -400,
    SignError = -105,
}

public class LoginResultPVIDto
{
    public const string STATUS_SUCCESS = "00";

    public string Status { get; set; }
    public string Message { get; set; }
    public UserContentPVIDto DataUser { get; set; }
    public string Token { get; set; }
    public bool Successed { get;  set; }

    public override string ToString()
    {
        return $"Status: {Status}, Message: {Message}";
    }

    public bool IsSuccessed => Status == STATUS_SUCCESS;

}
public class UserContentPVIDto
{
    public string UserName { get; set; }
    public string ma_user { get; set; } // Key cua tai khoan
    public string ten_user { get; set; }
    public string full_name { get; set; }
    public string ma_donvi { get; set; }
    public string ma_daily { get; set; }
    public string ma_phong { get; set; }
    public string ma_cbo { get; set; }
    public int to_chuc { get; set; }
    public string ma_chucvu { get; set; }
    public string dc_email { get; set; }
    public string ParentId { get; set; }
    public string ma_diadiem { get; set; }
    public long gia_tri { get; set; }
    public string ma_nhkenh { get; set; }
    public string ma_kenh { get; set; }
    public string loai_kenh { get; set; }

}