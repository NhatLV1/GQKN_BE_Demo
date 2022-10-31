using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PVI.GQKN.Infrastructure.EntityConfigrurations;

public class KhaiBaoTonThatEntityTypeConfiguration :
    DefaultEntityTypeConfiguration<KhaiBaoTonThat>
{
    public override void Configure(EntityTypeBuilder<KhaiBaoTonThat> builder)
    {
        base.Configure(builder);

        builder.ToTable("kbtt_ctu");

        builder.Property(e => e.MaDinhDanh).HasMaxLength(30).IsRequired(); //1
        builder.Property(e => e.HoTen).HasMaxLength(50).IsRequired(); // 2
        builder.Property(e => e.NguoiLienHe).HasMaxLength(50).IsRequired(); // 3
        builder.Property(e => e.DiaChi).HasMaxLength(250).IsRequired(false); // 4
        builder.Property(e => e.Email).HasMaxLength(50).IsRequired(); // 5
        builder.Property(e => e.SoDienThoai).HasMaxLength(50).IsRequired(); // 6
        builder.Property(e => e.SoHopDong).HasMaxLength(50).IsRequired(false); // 7
        builder.Property(e => e.SoDonBaoHiem).HasMaxLength(50).IsRequired(); // 8 
        builder.Property(e => e.DoiTuongBiTonThat).HasMaxLength(50).IsRequired(); // 9
        builder.Property(e => e.NgayBatDauBH).IsRequired(false);    // 10
        builder.Property(e => e.NgayKetThucBH).IsRequired(false);   // 11
        builder.Property(e => e.DoiTuongDuocBaoHiem).HasMaxLength(50).IsRequired(true); // 12
        builder.Property(e => e.DonViCapDonId).IsRequired(true);    // 13
        builder.Property(e => e.ThoiGianTonThat).IsRequired(); // 14
        builder.Property(e => e.MaTinhId).IsRequired(false); // 15
        builder.Property(e => e.DiaDiemTonThat).IsRequired(true).HasMaxLength(150); // 16
        builder.Property(e => e.UocLuongTonThat).IsRequired(false).HasPrecision(18, 2); // 17
        builder.Property(e => e.TyGiaId).IsRequired(false); // 18
        builder.Property(e => e.NguyenNhanSoBo).HasMaxLength(500).IsRequired(true); // 19
        builder.Property(e => e.PhuongAnKhacPhuc).HasMaxLength(500).IsRequired(true); // 20
        builder.Property(e => e.ThongTinKhac).HasMaxLength(500).IsRequired(false); // 21
        
        // thông tin tiếp nhận
        builder.Property(e => e.HinhThucId).IsRequired(false);
        builder.Property(e => e.ThoigianTiepNhan).IsRequired(false);
        builder.Property(e => e.DeXuat).HasMaxLength(250).IsRequired(false);
        builder.Property(e => e.TrangThai).IsRequired(true).HasDefaultValue(true);
        builder.Property(e => e.NguoiTaoId).IsRequired(false);

        builder.HasOne(e => e.DonViCapDon)
            .WithMany()
            .HasForeignKey(e => e.DonViCapDonId);


        builder.HasOne(e => e.GQKN)
            .WithMany()
            .HasForeignKey(e => e.GQKNId);

    }
}
