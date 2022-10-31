namespace PVI.GQKN.Infrastructure.EntityConfigrurations;

public class HoSoTonThatEntityTypeConfiguration :
    DefaultEntityTypeConfiguration<HoSoTonThat>
{
    public override void Configure(EntityTypeBuilder<HoSoTonThat> builder)
    {
        base.Configure(builder);

        builder.ToTable("tbl_ho_so_tt");

        builder.OwnsOne(x => x.NguoiDuocBaoHiem, sa => {
            sa.Property(l => l.HoTen).IsRequired().HasMaxLength(50);
            sa.Property(l => l.DiaChi).HasMaxLength(50);
        });
        builder.Navigation(l => l.NguoiDuocBaoHiem).IsRequired();

        builder.OwnsOne(x => x.NguoiLienHe, nb =>
        {
            nb.Property(l => l.HoTen).IsRequired().HasMaxLength(50);
            nb.Property(l => l.Email).HasMaxLength(50);
            nb.Property(l => l.SoDienThoai).IsRequired()
                .HasMaxLength(50);
        });
        builder.Navigation(l => l.NguoiLienHe).IsRequired();
        builder.HasOne(x => x.DonViCapDon)
         .WithMany(d => d.HoSoTonThats)
         .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.SoHopDong)
             .HasMaxLength(50)
             .HasComment("Số hợp đồng");

        builder.OwnsOne(x => x.DonBaoHiem, nb => {
            nb.Property(d => d.NgayBatDauBH).IsRequired().HasComment("Ngày bắt đầu bảo hiểm");
            nb.Property(d => d.NgayKetThucBH).IsRequired().HasComment("Ngày kết thúc bảo hiểm");
            nb.Property(d => d.SoDon).IsRequired()
                .HasMaxLength(50)
                .HasComment("Số đơn bảo hiểm");
        });

        builder.Property(x => x.DonViCapDonId)
            .IsRequired()
            .HasComment("Đơn vị cấp đơn");

        builder.Property(x => x.DoiTuongTonThat)
            .IsRequired()
            .HasMaxLength(250)
            .HasComment("Đối tượng bị tổn thất");

        builder.Property(x => x.ThoiGianTonThat).IsRequired()
            .HasComment("Thời gian tổn thất");
        builder.Property(x => x.DiaDiemTonThat).IsRequired()
            .HasMaxLength(150)
            .HasComment("Địa điểm tổn thất");
        builder.Property(x => x.UocLuongTonThat).HasPrecision(18, 2)
            .HasComment("Ước lượng tổn thất");
        builder.Property(x => x.DonViTienTe)
            .HasMaxLength(50)
            .HasComment("Đơn vị tiền tệ");

        builder.Property(x => x.NguyenNhanSoBo)
            .HasMaxLength(500)
            .HasComment("Nguyên nhân sơ bộ");
        builder.Property(x => x.PhuongAnKhacPhuc)
            .HasMaxLength(500)
            .HasComment("Phương án khắc phục");
        builder.Property(x => x.ThongTinKhac)
            .HasMaxLength(500)
            .HasComment("Thông tin khác");
        builder.Property(x => x.DeXuatDeNghi)
            .HasMaxLength(500)
            .HasComment("Đề nghị/đề xuất");

        builder.Property(e => e.TrangThaiHoSo)
            .HasComment("Trạng thái hồ sơ");

        builder.Property(e => e.NguoiTaoId)
            .IsRequired()
            .HasComment("Mã người tạo");
    }
}
