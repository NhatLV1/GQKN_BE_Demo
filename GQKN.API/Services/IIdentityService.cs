namespace PVI.GQKN.API.Services;


public interface IIdentityService
{

    public const string USER_ID_CLAIM_NAME = "Id";
    public const string DONVI_ID_CLAIM_NAME = "DonViId";

    string GetUserIdentity();

    int? GetUserId();

    string GetUserName();

    public int? GetDonViId();

    public bool IsSuperAdmin();
}

