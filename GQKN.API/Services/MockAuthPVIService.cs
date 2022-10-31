namespace PVI.GQKN.API.Services;

public class MockAuthPVIService : IAuthPVI
{
    public Task<DeleteUserPVIResult> DeleteUser(string userId)
    {
        throw new NotImplementedException();
    }

    public  Task<LoginResultPVIDto> Login(string username, string password, string type)
    {
        var mockResult = new LoginResultPVIDto()
        {
            Successed = true,
            Status = LoginResultPVIDto.STATUS_SUCCESS,
            DataUser = new UserContentPVIDto() 
            {
                dc_email = "fake@email.com",
                full_name = "fakeFullname",
                ma_cbo = "fake_ma_cb",
                ma_chucvu = null, // todo
                ma_phong = "00.11304000",
                ma_donvi = "00", // Trụ sở PVI
                ma_user = "fake_ma_user",
                ten_user = "Ho Va Ten",
                UserName = username,
            }
        };

        return Task.FromResult(mockResult);
    }

    public Task<RegisterUserPVIResult> RegisterUser(RegisterPVIUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateUserPVIResult> UpdateUser(UpdatePVIUserRequest content)
    {
        throw new NotImplementedException();
    }

    public Task<VerifyTokenResultPVIDto> VerifyToken(string token)
    {
        throw new NotImplementedException();
    }
}
