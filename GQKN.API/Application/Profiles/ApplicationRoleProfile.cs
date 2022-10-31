using PVI.GQKN.API.Application.Commands.VaiTroCommands;

namespace PVI.GQKN.API.Application.Profiles;

public class ApplicationRoleProfile: Profile
{
	public ApplicationRoleProfile()
	{
		CreateMap<ApplicationRole, VaiTroDto>()
			.ForMember(e => e.TenVaiTro, opt => opt.MapFrom(e => e.Name));

		CreateMap<CreateVaiTroRequest, ApplicationRole>()
			.ForMember(e => e.Name, opt => opt.MapFrom(e => e.TenVaiTro));

		CreateMap<UpdateVaiTroRequest, ApplicationRole>()
            .ForMember(e => e.Name, opt => opt.MapFrom(e => e.TenVaiTro)); ;

        CreateMap<ApplicationRole, VaiTroInfo>()
			.ForMember(e => e.TenVaiTro, opt => opt.MapFrom(v => v.Name));
    }
}
