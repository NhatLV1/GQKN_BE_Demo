namespace PVI.GQKN.Infrastructure.EntityConfigrurations;

public class PhongBanEntityTypeConfiguration :
    DefaultEntityTypeConfiguration<PhongBan>
{
    public override void Configure(EntityTypeBuilder<PhongBan> builder)
    {
        base.Configure(builder);

        builder.ToTable("tbl_phong_ban");

        builder.Property(x => x.TenPhongBan)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(x => x.PhongBanChaId)
            .IsRequired(false);

        builder.Property(p => p.LoaiPhongBan);

        builder.HasOne(x => x.PhongBanCha)
            .WithOne()
            .HasForeignKey<PhongBan>(x => x.PhongBanChaId)
            .IsRequired(false);

        builder.Property(e => e.DonViId);

        builder.Property(e => e.MaPhongBan)
            .HasMaxLength(15);


        builder.HasOne(e => e.DonVi)
            .WithMany()
            .HasForeignKey(x => x.DonViId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
