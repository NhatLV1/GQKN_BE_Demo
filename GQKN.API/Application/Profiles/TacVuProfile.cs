namespace PVI.GQKN.API.Application.Profiles;

public class TacVuProfile: Profile
{
	public TacVuProfile()
	{
		CreateMap<AclOperation, QuyenDto>();
	}
}
