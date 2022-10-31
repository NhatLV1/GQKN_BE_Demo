namespace PVI.GQKN.API.Application.Profiles;

public class LoginRequestProfile: Profile
{
	public LoginRequestProfile()
	{
		CreateMap<LoginRequest, GQKNLoginRequest>();
		CreateMap<LoginRequest, PiasLoginRequest>();
	}
}
