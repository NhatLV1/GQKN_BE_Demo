using NuGet.ContentModel;
using System.Security.Cryptography;

namespace PVI.GQKN.API.Services;

public class AuthPVIService : IAuthPVI
{
    private readonly HttpClient _apiClient;
    private readonly IOptions<AppSettings> _settings;
    private readonly ILogger<AuthPVIService> _logger;
    private readonly PiasCredentials _piasCredential;
    private readonly string _loginUserUrl;
    private readonly string _registerUserUrl;
    private readonly string _updateUserUrl;
    private readonly string _deleteUserUrl;
    private readonly string _verifyTokenUrl;

    public AuthPVIService(HttpClient httpClient,
        IOptions<AppSettings> settings,
        ILogger<AuthPVIService> logger)
    {
        _apiClient = httpClient;
        _settings = settings;
        _logger = logger;
        _piasCredential = _settings.Value.Pias ?? throw new ArgumentNullException(nameof(PiasCredentials));
        _loginUserUrl = $"{_piasCredential.Url}/API_App1/ManagerApplication/LoginSSO_PVI";
        _registerUserUrl = $"{_piasCredential.Url}/API_App1/ManagerApplication/RegisterUser_PVI";
        _updateUserUrl = $"{_piasCredential.Url}/API_App1/ManagerApplication/UpdateUser_PVI";
        _deleteUserUrl = $"{_piasCredential.Url}/API_App1/ManagerApplication/DeleteUser_PVI";
        _verifyTokenUrl = $"{_piasCredential.Url}/API_App1/ManagerApplication/VerifyToken";
    }

    public async Task<LoginResultPVIDto> Login(string username, string password, string type = IAuthPVI.LOGIN_TYPE_QLCD)
    {
        // Sign = MD5(Key + objData.UserName + objData.Password + objData.Type);
        var secret = $"{_piasCredential.Key}{username}{password}{type}";

        var md5Secret = secret.MD5Hash();
     
        LoginContentPVIDto content = new LoginContentPVIDto()
        {
            UserName = username,
            Password = password,
            CpId = _piasCredential.CpId,
            Type = type,
            Sign = md5Secret
        };

        var result = await this.Post<LoginResultPVIDto>(content, _loginUserUrl);
        
        return result;
        //var json = JsonSerializer.Serialize(content);
        //// var encryptedContent = Encrypt_Parameter(json, _piasCredential.Key);
        //var loginContent = new StringContent(json,
        //    Encoding.UTF8, "application/json");

        //var response = await _apiClient.PostAsync(_loginUserUrl, loginContent);
        //response.EnsureSuccessStatusCode();

        //var jsonResponse = await response.Content.ReadAsStringAsync();

        //return JsonSerializer.Deserialize<LoginResultPVIDto>(jsonResponse, new JsonSerializerOptions
        //{
        //    PropertyNameCaseInsensitive = true
        //});
    }

    private async Task<T> Post<T>(object content, string endpoint)
    {
        var json = JsonSerializer.Serialize(content);
        var stringContent = new StringContent(json,
            Encoding.UTF8, "application/json");

        var response = await _apiClient.PostAsync(endpoint, stringContent);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<VerifyTokenResultPVIDto> VerifyToken(string token)
    {
        var signedFootprint = $"{_piasCredential.Key}{token}".MD5Hash();
        VerifyTokenContentPVIDto content = new VerifyTokenContentPVIDto
        {
            CpId = _piasCredential.CpId,
            Token = token,
            Sign = signedFootprint,
        };

        var tokenContent = new StringContent(JsonSerializer.Serialize(content),
           Encoding.UTF8, "application/json");

        var response = await _apiClient.PostAsync(_verifyTokenUrl, tokenContent);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<VerifyTokenResultPVIDto>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public static string Encrypt_Parameter(string plainText, string KeyParameter_Security)
    {
        string encrypted = null;
        string key = KeyParameter_Security;
        try
        {
            byte[] inputBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
            byte[] pwdhash = null;
            //MD5CryptoServiceProvider hashmd5;

            //generate an MD5 hash from the password. 
            //a hash is a one way encryption meaning once you generate
            //the hash, you cant derive the password back from it.
            var hashmd5 = MD5.Create();
            pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            hashmd5 = null;

            // Create a new TripleDES service provider 
            var tdesProvider = TripleDES.Create();
            tdesProvider.Key = pwdhash;
            tdesProvider.Mode = CipherMode.ECB;

            encrypted = Convert.ToBase64String(
                tdesProvider.CreateEncryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length));
        }
        catch (Exception)
        {

            encrypted = string.Empty;
        }

        return encrypted;
    }

    public static string Decrypt_Parameter(string encryptedString, string KeyParameter_Security)
    {
        string decyprted = null;
        byte[] inputBytes = null;
        string key = KeyParameter_Security;
        try
        {
            inputBytes = Convert.FromBase64String(encryptedString);
            byte[] pwdhash = null;
           
            //generate an MD5 hash from the password. 
            //a hash is a one way encryption meaning once you generate
            //the hash, you cant derive the password back from it.
            var hashmd5 = MD5.Create();
            pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            hashmd5 = null;

            // Create a new TripleDES service provider 
            var tdesProvider = TripleDES.Create();
            tdesProvider.Key = pwdhash;
            tdesProvider.Mode = CipherMode.ECB;

            decyprted = ASCIIEncoding.ASCII.GetString(
                tdesProvider.CreateDecryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length));
        }
        catch (Exception)
        {
            decyprted = string.Empty;
        }

        return decyprted;
    }

    public async Task<UpdateUserPVIResult> UpdateUser(UpdatePVIUserRequest request)
    {
        var sign = $"{_piasCredential.Key}{request.UserId}{request.Password}".MD5Hash();
     
        UpdateUserPVI_Content data = new UpdateUserPVI_Content {
            ma_user = request.UserId,
            full_name = request.Fullname,
            password = request.Password,
            CpId = _piasCredential.CpId,
            Sign = sign
        };

        var result = await Post<UpdateUserPVIResult>(data, _updateUserUrl);

        return result;
    }

    public async Task<DeleteUserPVIResult> DeleteUser(string userId)
    {
        var sign = $"{_piasCredential.Key}{userId}".MD5Hash();

        DeleteUserPVI_Content content = new () { 
            ma_user = userId,
            CpId = _piasCredential.CpId,
            Sign = sign
        };

        var result = await Post<DeleteUserPVIResult>(content, _deleteUserUrl);

        return result;
    }

    public async Task<RegisterUserPVIResult> RegisterUser(RegisterPVIUserRequest r)
    {
        // Sign = MD5(Key + objData.ten_user + objData.password + objData.ma_donvi + objData.ma_phong + objData.email + objData.trang_thai.ToString());
        var sign = $"{_piasCredential.Key}{r.Username}{r.Password}{r.MaDonVi}{r.MaPhongBan}{r.Email}{r.Status}".MD5Hash();
        var content = new RegisterUserPVI_Content
        {
            ten_user = r.Username,
            full_name = r.Fullname,
            trang_thai = r.Status,
            password = r.Password,
            email = r.Email,
            ma_donvi = r.MaDonVi,
            ma_phong = r.MaPhongBan,
            CpId = _piasCredential.CpId,
            Sign = sign
        };

        var result = await Post<RegisterUserPVIResult>(content, _registerUserUrl);

        return result;
    }
}
