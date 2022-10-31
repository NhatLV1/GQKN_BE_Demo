namespace PVI.GQKN.Infrastructure.EntityConfigrurations;

internal class DonViEntityTypeConfiguration : DefaultEntityTypeConfiguration<DonVi>
{
    public override void Configure(EntityTypeBuilder<DonVi> builder)
    {
        base.Configure(builder);

        builder.ToTable("tbl_don_vi");

        builder.HasIndex(e => e.TenDonVi)
            .IsUnique(true);

        builder.HasIndex(e => e.MaDonVi)
            .IsUnique(true);

        builder.Property(e => e.TenDonVi)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.MaDonVi)
            .IsRequired(true)
            .HasMaxLength(10);

        builder.Property(e => e.MaTinh)
            .IsRequired(false)
            .HasMaxLength(10);

        builder.OwnsMany(e => e.Scopes);
    }
}
