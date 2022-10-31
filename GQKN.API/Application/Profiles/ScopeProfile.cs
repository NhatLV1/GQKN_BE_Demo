namespace PVI.GQKN.API.Application.Profiles;

public class ScopeProfile: Profile
{
	public ScopeProfile()
	{
		CreateMap<AclScope, Scope>()
			.ForMember(e => e.Code, opt => opt.MapFrom(e => e.Id));
			
		CreateMap<AclScope, ScopeDto>()
			.ForMember(e => e.ScopeId, opt => opt.MapFrom(e => e.Id))
			.ForMember(e => e.ScopeName, opt => opt.MapFrom(e => e.Name))
			.ForMember(e => e.ScopeOrder, opt => opt.MapFrom(e => e.Order))
			.ReverseMap();

	}
}
