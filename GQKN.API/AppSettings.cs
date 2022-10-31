namespace PVI.GQKN.API;

public class AppSettings
{
    public const string KEY_EMAIL_RESET_PASSWORD = "email:reset_password";

    public bool UseCustomizationData { get; set; }

    public string EventBusConnection { get; set; }

    public PiasCredentials Pias { get;  set; }
}


public class PiasCredentials
{
    public string Url { get; set; }
    public string CpId { get; set; }
    public string Key { get; set; }
}