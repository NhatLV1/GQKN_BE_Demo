using PVI.GQKN.Infrastructure.Idempotency;

namespace PVI.GQKN.Infrastructure.EntityConfigrurations;

internal class BieuMauEntityConfiguration :
    DefaultEntityTypeConfiguration<BieuMau>
{
    public override void Configure(EntityTypeBuilder<BieuMau> builder)
    {
        base.Configure(builder);

        builder.ToTable("tbl_bieu_mau");

        builder.Property(e => e.TenBieuMau)
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(e => e.NoiDung)
            .IsRequired(true);
    }
}
