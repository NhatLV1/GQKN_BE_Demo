namespace PVI.GQKN.API.Application.Profiles;

public class DonViProfile: Profile
{
	public DonViProfile()
	{
		CreateMap<CreateDonViRequest, DonVi>()
			.ForMember(e => e.Scopes, opt => opt.Ignore());
        CreateMap<UpdateDonViRequest, DonVi>()
			.ForMember(e => e.Scopes, opt => opt.Ignore())
			.ForMember(e => e.Id, opt => opt.Ignore());
		CreateMap<DonVi, DonViDto>();
	}
}
