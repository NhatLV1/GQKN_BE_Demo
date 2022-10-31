using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVI.GQKN.API.Infrastructure.GQKNDbContextMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gqkn");

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                    DataProtected = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                schema: "gqkn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_bieu_mau",
                columns: table => new
                {
                    pr_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBieuMau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_user_tao = table.Column<int>(type: "int", nullable: true),
                    ma_user_sua = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_bieu_mau", x => x.pr_key);
                });

            migrationBuilder.CreateTable(
                name: "tbl_chuc_danh",
                columns: table => new
                {
                    pr_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChucVu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_user_tao = table.Column<int>(type: "int", nullable: true),
                    ma_user_sua = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_chuc_danh", x => x.pr_key);
                });

            migrationBuilder.CreateTable(
                name: "tbl_don_vi",
                columns: table => new
                {
                    pr_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonVi = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenDonVi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_user_tao = table.Column<int>(type: "int", nullable: true),
                    ma_user_sua = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_don_vi", x => x.pr_key);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ho_so_tt",
                columns: table => new
                {
                    pr_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDuocBaoHiem_HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiDuocBaoHiem_DiaChi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NguoiDuocBaoHiem_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiDuocBaoHiem_SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiLienHe_HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiLienHe_DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiLienHe_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NguoiLienHe_SoDienThoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoHopDong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Số hợp đồng"),
                    DonBaoHiem_SoDon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Số đơn bảo hiểm"),
                    DonBaoHiem_SDBD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonBaoHiem_NgayBatDauBH = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày bắt đầu bảo hiểm"),
                    DonBaoHiem_NgayKetThucBH = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày kết thúc bảo hiểm"),
                    DonViCapDonId = table.Column<int>(type: "int", nullable: false, comment: "Đơn vị cấp đơn"),
                    DoiTuongTonThat = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Đối tượng bị tổn thất"),
                    ThoiGianTonThat = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Thời gian tổn thất"),
                    DiaDiemTonThat = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Địa điểm tổn thất"),
                    UocLuongTonThat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, comment: "Ước lượng tổn thất"),
                    DonViTienTe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Đơn vị tiền tệ"),
                    NguyenNhanSoBo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Nguyên nhân sơ bộ"),
                    PhuongAnKhacPhuc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Phương án khắc phục"),
                    ThongTinKhac = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Thông tin khác"),
                    DeXuatDeNghi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Đề nghị/đề xuất"),
                    TrangThaiHoSo = table.Column<bool>(type: "bit", nullable: false, comment: "Trạng thái hồ sơ"),
                    TrangThaiTaiLieu = table.Column<int>(type: "int", nullable: false),
                    TaiLieuConThieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiTaoId = table.Column<int>(type: "int", nullable: false, comment: "Mã người tạo"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_user_sua = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ho_so_tt", x => x.pr_key);
                    table.ForeignKey(
                        name: "FK_tbl_ho_so_tt_tbl_don_vi_DonViCapDonId",
                        column: x => x.DonViCapDonId,
                        principalTable: "tbl_don_vi",
                        principalColumn: "pr_key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_phong_ban",
                columns: table => new
                {
                    pr_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhongBan = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhongBanChaId = table.Column<int>(type: "int", nullable: true),
                    LoaiPhongBan = table.Column<int>(type: "int", nullable: false),
                    DonViId = table.Column<int>(type: "int", nullable: true),
                    MaPhongBan = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_user_tao = table.Column<int>(type: "int", nullable: true),
                    ma_user_sua = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_phong_ban", x => x.pr_key);
                    table.ForeignKey(
                        name: "FK_tbl_phong_ban_tbl_don_vi_DonViId",
                        column: x => x.DonViId,
                        principalTable: "tbl_don_vi",
                        principalColumn: "pr_key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_phong_ban_tbl_phong_ban_PhongBanChaId",
                        column: x => x.PhongBanChaId,
                        principalTable: "tbl_phong_ban",
                        principalColumn: "pr_key");
                });

            migrationBuilder.CreateTable(
                name: "thu_muc",
                columns: table => new
                {
                    pr_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_kbtt = table.Column<int>(type: "int", nullable: false),
                    ten_thu_muc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_user_tao = table.Column<int>(type: "int", nullable: true),
                    ma_user_sua = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thu_muc", x => x.pr_key);
                    table.ForeignKey(
                        name: "FK_thu_muc_tbl_ho_so_tt_ma_kbtt",
                        column: x => x.ma_kbtt,
                        principalTable: "tbl_ho_so_tt",
                        principalColumn: "pr_key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonViId = table.Column<int>(type: "int", nullable: true),
                    PhongBanId = table.Column<int>(type: "int", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_tbl_don_vi_DonViId",
                        column: x => x.DonViId,
                        principalTable: "tbl_don_vi",
                        principalColumn: "pr_key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_tbl_phong_ban_PhongBanId",
                        column: x => x.PhongBanId,
                        principalTable: "tbl_phong_ban",
                        principalColumn: "pr_key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Họ và tên"),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Ảnh đại diện"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ngay_sinh = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày sinh"),
                    dia_chi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Địa chỉ"),
                    ma_donvi = table.Column<int>(type: "int", nullable: true, comment: "Mã đơn vị"),
                    ma_chucdanh = table.Column<int>(type: "int", nullable: true, comment: "Chức danh FK"),
                    ma_phong = table.Column<int>(type: "int", nullable: true, comment: "Phòng ban FK"),
                    MaNhom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Mã nhóm"),
                    quan_tri = table.Column<bool>(type: "bit", nullable: false, comment: "Tài khoản admin hay không?"),
                    trang_thai_user = table.Column<bool>(type: "bit", nullable: false, comment: "Trạng thái user (active?)"),
                    loai_tai_khoan = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_dinhdanh = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_tbl_chuc_danh_ma_chucdanh",
                        column: x => x.ma_chucdanh,
                        principalTable: "tbl_chuc_danh",
                        principalColumn: "pr_key");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_tbl_don_vi_ma_donvi",
                        column: x => x.ma_donvi,
                        principalTable: "tbl_don_vi",
                        principalColumn: "pr_key");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_tbl_phong_ban_ma_phong",
                        column: x => x.ma_phong,
                        principalTable: "tbl_phong_ban",
                        principalColumn: "pr_key");
                });

            migrationBuilder.CreateTable(
                name: "kbtt_ctu",
                columns: table => new
                {
                    pr_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_dinhdanh = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ho_ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nguoi_lhe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dia_chi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    so_dthoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    so_hdong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    so_don_bh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    so_don_sdbs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtuong_bi_tt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_bdau_bh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngay_kthuc_bh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtuong_duoc_bh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    donvi_gqkn = table.Column<int>(type: "int", nullable: true),
                    donvi_cdon = table.Column<int>(type: "int", nullable: false),
                    tgian_tt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ma_tinhtp = table.Column<int>(type: "int", nullable: true),
                    dia_diem_tt = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    uoc_luong_tt = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ma_tygia = table.Column<int>(type: "int", nullable: true),
                    nnhan_so_bo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    pa_kphuc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ttin_khac = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ma_httn = table.Column<int>(type: "int", nullable: true),
                    tgian_tnhan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_user_nhan = table.Column<int>(type: "int", nullable: true),
                    de_xuat = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    tthai_kbtt = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    TrangThaiTaiLieu = table.Column<int>(type: "int", nullable: false),
                    TaiLieuConThieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhongBanId = table.Column<int>(type: "int", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_user_tao = table.Column<int>(type: "int", nullable: true),
                    ma_user_sua = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kbtt_ctu", x => x.pr_key);
                    table.ForeignKey(
                        name: "FK_kbtt_ctu_tbl_don_vi_donvi_cdon",
                        column: x => x.donvi_cdon,
                        principalTable: "tbl_don_vi",
                        principalColumn: "pr_key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kbtt_ctu_tbl_don_vi_donvi_gqkn",
                        column: x => x.donvi_gqkn,
                        principalTable: "tbl_don_vi",
                        principalColumn: "pr_key");
                    table.ForeignKey(
                        name: "FK_kbtt_ctu_tbl_phong_ban_PhongBanId",
                        column: x => x.PhongBanId,
                        principalTable: "tbl_phong_ban",
                        principalColumn: "pr_key");
                });

            migrationBuilder.CreateTable(
                name: "dm_tailieu",
                columns: table => new
                {
                    pr_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_kbtt = table.Column<int>(type: "int", nullable: false),
                    thu_muc_id = table.Column<int>(type: "int", nullable: false),
                    ten_tai_lieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    duong_dan = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_user_tao = table.Column<int>(type: "int", nullable: true),
                    ma_user_sua = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dm_tailieu", x => x.pr_key);
                    table.ForeignKey(
                        name: "FK_dm_tailieu_tbl_ho_so_tt_ma_kbtt",
                        column: x => x.ma_kbtt,
                        principalTable: "tbl_ho_so_tt",
                        principalColumn: "pr_key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dm_tailieu_thu_muc_thu_muc_id",
                        column: x => x.thu_muc_id,
                        principalTable: "thu_muc",
                        principalColumn: "pr_key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tailieu_bookmark",
                columns: table => new
                {
                    pr_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_tlieu = table.Column<int>(type: "int", nullable: false),
                    ten_bookmark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ddan_bookmark = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ma_bookmark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ma_user_tao = table.Column<int>(type: "int", nullable: true),
                    ma_user_sua = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tailieu_bookmark", x => x.pr_key);
                    table.ForeignKey(
                        name: "FK_tailieu_bookmark_dm_tailieu_ma_tlieu",
                        column: x => x.ma_tlieu,
                        principalTable: "dm_tailieu",
                        principalColumn: "pr_key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_DonViId",
                table: "AspNetRoles",
                column: "DonViId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_PhongBanId",
                table: "AspNetRoles",
                column: "PhongBanId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ma_chucdanh",
                table: "AspNetUsers",
                column: "ma_chucdanh");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ma_donvi",
                table: "AspNetUsers",
                column: "ma_donvi");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ma_phong",
                table: "AspNetUsers",
                column: "ma_phong");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_dm_tailieu_Guid",
                table: "dm_tailieu",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dm_tailieu_ma_kbtt",
                table: "dm_tailieu",
                column: "ma_kbtt");

            migrationBuilder.CreateIndex(
                name: "IX_dm_tailieu_thu_muc_id",
                table: "dm_tailieu",
                column: "thu_muc_id");

            migrationBuilder.CreateIndex(
                name: "IX_kbtt_ctu_donvi_cdon",
                table: "kbtt_ctu",
                column: "donvi_cdon");

            migrationBuilder.CreateIndex(
                name: "IX_kbtt_ctu_donvi_gqkn",
                table: "kbtt_ctu",
                column: "donvi_gqkn");

            migrationBuilder.CreateIndex(
                name: "IX_kbtt_ctu_Guid",
                table: "kbtt_ctu",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_kbtt_ctu_PhongBanId",
                table: "kbtt_ctu",
                column: "PhongBanId");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_tailieu_bookmark_Guid",
                table: "tailieu_bookmark",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tailieu_bookmark_ma_tlieu",
                table: "tailieu_bookmark",
                column: "ma_tlieu");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_bieu_mau_Guid",
                table: "tbl_bieu_mau",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_chuc_danh_Guid",
                table: "tbl_chuc_danh",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_chuc_danh_MaChucVu",
                table: "tbl_chuc_danh",
                column: "MaChucVu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_don_vi_Guid",
                table: "tbl_don_vi",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_don_vi_MaDonVi",
                table: "tbl_don_vi",
                column: "MaDonVi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_don_vi_TenDonVi",
                table: "tbl_don_vi",
                column: "TenDonVi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ho_so_tt_DonViCapDonId",
                table: "tbl_ho_so_tt",
                column: "DonViCapDonId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ho_so_tt_Guid",
                table: "tbl_ho_so_tt",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_phong_ban_DonViId",
                table: "tbl_phong_ban",
                column: "DonViId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_phong_ban_Guid",
                table: "tbl_phong_ban",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_phong_ban_PhongBanChaId",
                table: "tbl_phong_ban",
                column: "PhongBanChaId",
                unique: true,
                filter: "[PhongBanChaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_thu_muc_Guid",
                table: "thu_muc",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_thu_muc_ma_kbtt",
                table: "thu_muc",
                column: "ma_kbtt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "kbtt_ctu");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "requests",
                schema: "gqkn");

            migrationBuilder.DropTable(
                name: "tailieu_bookmark");

            migrationBuilder.DropTable(
                name: "tbl_bieu_mau");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "dm_tailieu");

            migrationBuilder.DropTable(
                name: "tbl_chuc_danh");

            migrationBuilder.DropTable(
                name: "tbl_phong_ban");

            migrationBuilder.DropTable(
                name: "thu_muc");

            migrationBuilder.DropTable(
                name: "tbl_ho_so_tt");

            migrationBuilder.DropTable(
                name: "tbl_don_vi");
        }
    }
}
