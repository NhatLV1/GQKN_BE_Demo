using Microsoft.EntityFrameworkCore.Metadata;

namespace PVI.GQKN.Infrastructure.EntityConfigrurations;

public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.UserId)
            .IsUnique();

        builder.Property(e => e.HoTen)
            .HasMaxLength(50)
            .IsRequired(false)
            .HasComment("Họ và tên");

        builder.Property(e => e.AnhDaiDien)
            .HasMaxLength(50)
            .IsRequired(false)
            .HasComment("Ảnh đại diện");

        builder.Property(e => e.NgaySinh)
           .IsRequired(false)
           .HasComment("Ngày sinh");

        builder.Property(e => e.DiaChi)
           .HasMaxLength(100)
           .IsRequired(false)
           .HasComment("Địa chỉ");

        builder.Property(e => e.DonViId)
            .IsRequired(false)
            .HasComment("Mã đơn vị");

        builder.Property(e => e.MaNhom)
            .IsRequired(false)
            .HasMaxLength(50)
            .HasComment("Mã nhóm");


        builder.Property(e => e.QuanTri)
            .HasComment("Tài khoản admin hay không?");
        // SĐT => Inherits => IdentityUser
        // Email => Inherits => IdentityUser

        builder.Property(e => e.TrangThaiUser)
            .HasComment("Trạng thái user (active?)");

        builder.Property(e => e.ChucDanhId)
            .IsRequired(false)
            .HasComment("Chức danh FK");

        builder.Property(e => e.PhongBanId)
            .IsRequired(false)
            .HasComment("Phòng ban FK");

        builder.Property(e => e.AccountType)
            .HasDefaultValue(AccountType.PIAS);

        builder.Property(e => e.MaUserPVI)
            .HasMaxLength(15);

        builder.Property(e => e.UserId)
            .ValueGeneratedOnAdd()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

        // FK
        builder.HasOne(e => e.ChucDanh)
            .WithMany()
            .HasForeignKey(e => e.ChucDanhId);
        builder.HasOne(e => e.PhongBan)
            .WithMany()
            .HasForeignKey(e => e.PhongBanId);

        // Default
        builder.Property(x => x.NgayTao).HasDefaultValueSql("getdate()"); // SQL Server
        builder.Property(x => x.NgaySua).ValueGeneratedOnAddOrUpdate()
            .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Save);
    }
}
