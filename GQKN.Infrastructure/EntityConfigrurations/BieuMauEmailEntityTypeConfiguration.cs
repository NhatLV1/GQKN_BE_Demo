using PVI.GQKN.Infrastructure.Idempotency;

namespace PVI.GQKN.Infrastructure.EntityConfigrurations;

public class BieuMauEmailEntityTypeConfiguration
    : IEntityTypeConfiguration<BieuMauEmail>
{
    public  void Configure(EntityTypeBuilder<BieuMauEmail> builder)
    {
        builder.ToTable("tbl_bieu_mau");

        builder.Property(e => e.TieuDe)
            .HasMaxLength(150)
            .IsRequired(true);
    }
}
