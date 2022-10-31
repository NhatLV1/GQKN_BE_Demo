namespace PVI.GQKN.API.Services.ModelDTOs;

public class VerifyTokenContentPVIDto
{
    public string Token { get; set; }
    public string CpId { get; set; }
    public string Sign { get; set; }

}

public class VerifyTokenResultPVIDto
{
    public string Status { get; set; }
    public string Message { get; set; }
}
