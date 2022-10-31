namespace PVI.GQKN.API.Application.Profiles;

public class ChucVuProfile: Profile
{
	public ChucVuProfile()
	{
		CreateMap<ChucDanh, ChucDanhInfo>().ReverseMap();
	}
}
