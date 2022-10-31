namespace PVI.GQKN.API.Application.Profiles;

public class PhongBanProfile: Profile
{
	public PhongBanProfile()
	{
        CreateMap<CreatePhongBanCommand, PhongBan>();
        CreateMap<UpdatePhongBanCommand, PhongBan>();
        CreateMap<PhongBan, PhongBanDto>();
        CreateMap<PhongBan, PhongBanInfo>();
    }
}
