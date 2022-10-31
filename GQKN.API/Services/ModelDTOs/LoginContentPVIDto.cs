namespace PVI.GQKN.API.Services.ModelDTOs;

public class LoginContentPVIDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string CpId { get; set; }
    public string Type { get; set; } //Type="QLCD" tài khoản PVI, "MAIL_PVI" mail pvi
    public string Sign { get; set; }

}
