using System.IdentityModel.Tokens.Jwt;

namespace PVI.GQKN.API.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration config;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IIdentityService identityService;
    private readonly IDonViRepository donViRepository;

    public AuthService(
        IConfiguration config,
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IIdentityService identityService,
        IDonViRepository donViRepository
        )
    {
        this.config = config;
        this.roleManager = roleManager;
        this.userManager = userManager;
        this.identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        this.donViRepository = donViRepository ?? throw new ArgumentNullException(nameof(donViRepository)); ;
    }

    private static List<AclScope> Scopes = new List<AclScope>()
    {
        new AclScope(1, ADMIN_OPS.ACL_SCOPE, "Quản trị hệ thống"),
        new AclScope(2, KBTT_OPS.ACL_SCOPE, "Khai báo tổn thất"),
        new AclScope(3, BCTT_OPS.ACL_SCOPE, "Báo cáo tổn thất"),
    };

    private List<AclResource> Resources = new List<AclResource>()
    {
        // (Order, Id,  Resource, Name, Scope)
        // Admin
        new AclResource(1, ADMIN_OPS.RES_ACCOUNT,  "Tài khoản người dùng", ADMIN_OPS.ACL_SCOPE),
        new AclResource(2, ADMIN_OPS.RES_VAITRO,   "Vai trò người dùng", ADMIN_OPS.ACL_SCOPE),
        new AclResource(3, ADMIN_OPS.RES_CHUCDANH, "Chức danh người dùng", ADMIN_OPS.ACL_SCOPE),
        new AclResource(4, ADMIN_OPS.RES_PHONGBAN, "Danh mục phòng ban", ADMIN_OPS.ACL_SCOPE),
        
        // KBTT
        new AclResource(5, KBTT_OPS.RES_KBTT, "Khai báo tổn thất", KBTT_OPS.ACL_SCOPE),
        new AclResource(6, KBTT_OPS.RES_FILE, "Upload file khai báo tổn thất", KBTT_OPS.ACL_SCOPE),

        // BCTT
        new AclResource(7, BCTT_OPS.RES_BCTT,   "Báo cáo tổn thất", BCTT_OPS.ACL_SCOPE),
        new AclResource(8, BCTT_OPS.RES_FOLDER, "Thư mục hồ sơ", BCTT_OPS.ACL_SCOPE),
        new AclResource(9, BCTT_OPS.RES_FILE,   "Upload file hồ sơ", BCTT_OPS.ACL_SCOPE),
        new AclResource(10, BCTT_OPS.RES_FLOW,   "Quy trình xét duyệt", BCTT_OPS.ACL_SCOPE),
    };

    private static List<AclOperation> DanhSachTacVu = new List<AclOperation> {
        // Account
        new AclOperation(1, ADMIN_OPS.ACCOUNT_LIST,  ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_ACCOUNT, "Danh mục tài khoản", "ADMIN-ACC-R"),
        new AclOperation(2, ADMIN_OPS.ACCOUNT_CREATE,  ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_ACCOUNT, "Tạo tài khoản người dùng", "ADMIN-ACC-C"),
        new AclOperation(3, ADMIN_OPS.ACCOUNT_UPDATE,  ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_ACCOUNT, "Cập nhật tài khoản người dùng", "ADMIN-ACC-U"),
        // Vai tròs
        new AclOperation(4, ADMIN_OPS.VAITRO_LIST,   ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_ACCOUNT, "Danh mục vai trò người dùng", "ADMIN-ROLE-R"),
        new AclOperation(5, ADMIN_OPS.VAITRO_CREATE,   ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_VAITRO, "Tạo vai trò người dùng", "ADMIN-ROLE-C"),
        new AclOperation(6, ADMIN_OPS.VAITRO_UPDATE,   ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_VAITRO, "Cập nhật vai trò người dùng", "ADMIN-ROLE-U"),
        new AclOperation(7, ADMIN_OPS.VAITRO_DELETE,   ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_VAITRO, "Xóa vai trò người dùng", "ADMIN-ROLE-D"),
        // Chức danh
        new AclOperation(8, ADMIN_OPS.CHUCDANH_LIST,   ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_CHUCDANH, "Truy xuất danh mục chức danh", "ADMIN-POS-R"),
        new AclOperation(9, ADMIN_OPS.CHUCDANH_CREATE,   ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_CHUCDANH, "Tạo chức danh mới", "ADMIN-POS-C"),
        new AclOperation(10, ADMIN_OPS.CHUCDANH_UPDATE,   ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_CHUCDANH, "Cập nhật chức danh", "ADMIN-POS-U"),
        new AclOperation(11, ADMIN_OPS.CHUCDANH_DELETE,   ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_CHUCDANH, "Xóa chức danh", "ADMIN-POS-D"),
        // Phòng ban
        new AclOperation(12, ADMIN_OPS.PHONGBAN_LIST,      ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_PHONGBAN, "Truy cập danh sách phòng ban", "ADMIN-DEP-R"),
        new AclOperation(13, ADMIN_OPS.PHONGBAN_CREATE,    ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_PHONGBAN, "Tạo phòng ban mới", "ADMIN-DEP-C"),
        new AclOperation(14, ADMIN_OPS.PHONGBAN_UPDATE,    ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_PHONGBAN, "Cập nhật phòng ban", "ADMIN-DEP-U"),
        new AclOperation(15, ADMIN_OPS.PHONGBAN_DELETE,    ADMIN_OPS.ACL_SCOPE, ADMIN_OPS.RES_PHONGBAN, "Xóa phòng ban", "ADMIN-DEP-D"),

        // Khai báo tổn thất
        new AclOperation(30, KBTT_OPS.LIST_KBTT,    KBTT_OPS.ACL_SCOPE, KBTT_OPS.RES_KBTT, "Danh sách tổn thất", "KBTT-CRUD-R"),
        new AclOperation(31, KBTT_OPS.CREATE_KBTT,  KBTT_OPS.ACL_SCOPE, KBTT_OPS.RES_KBTT, "Khai báo tổn thất", "KBTT-CRUD-C"),
        new AclOperation(32, KBTT_OPS.UPDATE_KBTT,  KBTT_OPS.ACL_SCOPE, KBTT_OPS.RES_KBTT, "Cập nhật Khai báo tổn thất", "KBTT-CRUD-U"),
        // Upload file khai báo tổn thất
        new AclOperation(33, KBTT_OPS.UPLOAD_FILE,   KBTT_OPS.ACL_SCOPE, KBTT_OPS.RES_FILE, "Upload tài liệu hồ sơ tổn thất", "KBTT-FILE-C"),
        new AclOperation(34, KBTT_OPS.DELETE_FILE,   KBTT_OPS.ACL_SCOPE, KBTT_OPS.RES_FILE, "Xóa tài liệu hồ sơ tổn thất", "KBTT-FILE-D"),
        new AclOperation(35, KBTT_OPS.EXPORT_FILE,   KBTT_OPS.ACL_SCOPE, KBTT_OPS.RES_FILE, "Tải về tài liệu hồ sơ tổn thất", "KBTT-FILE-E"),
         // Thư mục khai báo tổn thất
        new AclOperation(33, KBTT_OPS.FOLDER_CREATE,   KBTT_OPS.ACL_SCOPE, KBTT_OPS.RES_FOLDER, "Thêm thư mục khai báo tổn thất", "KBTT-FOLDER-C"),
        new AclOperation(34, KBTT_OPS.FOLDER_UPDATE,   KBTT_OPS.ACL_SCOPE, KBTT_OPS.RES_FOLDER, "Cập nhật thư mục khai báo tổn thất", "KBTT-FOLDER-U"),
        new AclOperation(35, KBTT_OPS.FOLDER_DELETE,   KBTT_OPS.ACL_SCOPE, KBTT_OPS.RES_FOLDER, "Xóa thư mục khai báo tổn thất", "KBTT-FOLDER-D"),

        // bctt
        new AclOperation(50, BCTT_OPS.BCTT_LIST,     BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_BCTT, "Danh sách báo cáo tổn thất", "BCTT-CRUD-R"),
        new AclOperation(51, BCTT_OPS.BCTT_CREATE,   BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_BCTT, "Tiếp nhận và lập BCTT", "BCTT-CRUD-C"),
        new AclOperation(51, BCTT_OPS.BCTT_UPDATE,   BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_BCTT, "Cập nhật thông tin BCTT", "BCTT-CRUD-U"),
        new AclOperation(53, BCTT_OPS.BCTT_CANCEL,   BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_BCTT, "Hủy hồ sơ", "BCTT-CRUD-D"),
        // bctt-file
        new AclOperation(55, BCTT_OPS.FILE_UPLOAD,   BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FILE, "Upload file hồ sơ tổn thất", "BCTT-FILE-C"),
        new AclOperation(56, BCTT_OPS.FILE_REMOVE,   BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FILE, "Delete file hồ sơ tổn thất", "BCTT-FILE-D"),
        new AclOperation(57, BCTT_OPS.FILE_EXPORT,   BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FILE, "Export file hồ sơ tổn thất", "BCTT-FILE-E"),
        // bctt-flow
        new AclOperation(59, BCTT_OPS.FLOW_VALIDATE_BCTT,   BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FLOW, "Xác nhận báo cáo tổn thất", "F-02.02"),
        new AclOperation(60, BCTT_OPS.FLOW_VALIDATE_KTTC,   BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FLOW, "Xác nhận thông tin phí bảo hiểm", "F-02-03"),
        new AclOperation(61, BCTT_OPS.FLOW_VALIDATE_TBH,   BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FLOW, "Xác nhận tái cơ cấu", "F-02-04"),
        new AclOperation(62, BCTT_OPS.FLOW_APROVE,         BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FLOW, "Duyệt báo cáo", "F-02-05"),
        new AclOperation(63, BCTT_OPS.FLOW_FORWARD,        BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FLOW, "Chuyển GQKN theo phân cấp BT", "F-02-05-F"),
        // folder 
        //new AclOperation(64, BCTT_OPS.FOLDER_CREATE,        BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FOLDER, "Tạo thư mục hồ sơ", ),
        //new AclOperation(64, BCTT_OPS.FOLDER_UPDATE,        BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FOLDER, "Cập nhật thư mục hồ sơ"),
        //new AclOperation(64, BCTT_OPS.FOLDER_DELETE,        BCTT_OPS.ACL_SCOPE, BCTT_OPS.RES_FOLDER, "Xóa thư mục hồ sơ"),
    };

    public ulong AggregateClaim(string group)
    {
        return this.GetACL()
            .Where(e => e.Scope == group)
            .Aggregate(0UL, (acc, x) => acc | x.Id);
    }

    public IReadOnlyCollection<AclOperation> GetACL() => DanhSachTacVu.AsReadOnly(); // TODO: localization resource

    private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
    {
        IdentityOptions _options = new IdentityOptions();

        // TODO: review
        var claims = new List<Claim>()
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(IIdentityService.USER_ID_CLAIM_NAME, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUnixEpoch().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName)
        };

        if(user.DonViId != null)
        {
            claims.Add(new Claim(IIdentityService.DONVI_ID_CLAIM_NAME, user.DonViId?.ToString()));
        }

        var userClaims = await userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        var userRoles = await userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
            var role = await roleManager.FindByNameAsync(userRole);
            if (role != null)
            {
                var roleClaims = await roleManager.GetClaimsAsync(role);
                foreach (Claim roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }
            }
        }
        return claims;
    }

    public async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var key = config["Jwt:Key"];
        var exprires = int.Parse(config["Jwt:Expires"]);
        var issuer = config["Jwt:Issuer"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = await GetUserClaims(user);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(exprires),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public IReadOnlyCollection<AclResource> GetResources()
    {
        return Resources.AsReadOnly();
    }

    public IReadOnlyCollection<AclScope> GetScopes()
    {
        return Scopes.AsReadOnly();
    }

    public async Task<IEnumerable<string>> GetUserScopes()
    {
        // superadmin => full scopes
        if (this.identityService.IsSuperAdmin())
            return GetScopes().Select(e => e.Id);

        var donViId = identityService.GetDonViId();
        if (donViId == null)
            return new List<string>();

        var donVi = await donViRepository.GetByIdAsync(donViId.Value);
        var donViScopes = donVi.Scopes.Select(e => e.Code);

        return donViScopes;
    }

}
