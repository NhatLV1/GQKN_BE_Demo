namespace PVI.GQKN.API.Application.Profiles;

public class KhaiBaoTonThatProfile: Profile
{
	public KhaiBaoTonThatProfile()
	{
		CreateMap<KhaiBaoTonThat, KhaiBaoTonThatKhachHangInfo>();
		CreateMap<CreateKBTTKhachHangRequest, KhaiBaoTonThat>();
		CreateMap<KhaiBaoTonThat, KhaiBaoTonThatKhachHangDto>();
	}
}
