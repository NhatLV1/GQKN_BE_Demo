
namespace PVI.GQKN.API.Application.Profiles
{
    public class ApplicationUserProfile: Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<CreateUserRequest, ApplicationUser>()
                .ForMember(e => e.PhoneNumber, opt => opt.MapFrom(c => c.SoDienThoai))
                .ForMember(e => e.UserName, opt => opt.MapFrom(c => c.Username));

            CreateMap<ApplicationUser, UserDto>()
                .ForMember(e => e.SoDienThoai, opt => opt.MapFrom(c => c.PhoneNumber))
                .ForMember(e => e.Guid, opt => opt.MapFrom(c => c.Id))
                .ForMember(e => e.Id, opt => opt.MapFrom(c => c.UserId));

            CreateMap<UpdateUserRequest, ApplicationUser>()
                .ForMember(e => e.PhoneNumber, opt => opt.MapFrom(c => c.SoDienThoai));

            CreateMap<ApplicationUser, UserProfile>()
                .ForMember(e => e.SoDienThoai, opt => opt.MapFrom(c => c.PhoneNumber))
                .ForMember(e => e.Guid, opt => opt.MapFrom(c => c.Id))
                .ForMember(e => e.Id, opt => opt.MapFrom(c => c.UserId));

            CreateMap<ApplicationUser, UserInfo>()
                .ForMember(e => e.Guid, opt => opt.MapFrom(c => c.Id))
                .ForMember(e => e.SoDienThoai, opt => opt.MapFrom(c => c.PhoneNumber))
                .ForMember(e => e.Id, opt => opt.MapFrom(u => u.UserId)).ReverseMap();

            CreateMap<UserContentPVIDto, ApplicationUser>()
                .ForMember(e => e.Email, opt => opt.MapFrom(e => e.dc_email))
                .ForMember(e => e.HoTen, opt => opt.MapFrom(e => e.full_name))
                .ForMember(e => e.MaUserPVI, opt => opt.MapFrom(e => e.ma_user))
                .ForMember(e => e.UserName, opt => opt.MapFrom(e => e.UserName))
                //.ForMember(e => e.ChucDanhId, opt => opt.MapFrom(e => e.ma_chucvu))
                //.ForMember(e => e.PhongBanId, opt => opt.MapFrom(e => e.ma_phong))

                //.ForMember(e => e.Email, opt => opt.MapFrom(e => e.ten_user))
                //.ForMember(e => e.Email, opt => opt.MapFrom(e => e.to_chuc))
            ;

        }
    }
}
