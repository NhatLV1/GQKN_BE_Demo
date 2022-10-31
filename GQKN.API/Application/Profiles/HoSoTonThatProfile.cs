namespace PVI.GQKN.API.Application.Profiles;

public class HoSoTonThatProfile: Profile
{
	public HoSoTonThatProfile()
	{
		CreateMap<HoSoTonThat, CreateKBTTKhachHangRequest>().ReverseMap();
		CreateMap<HoSoTonThat, HoSoTonThatKhachHangDto>();
	}
}
