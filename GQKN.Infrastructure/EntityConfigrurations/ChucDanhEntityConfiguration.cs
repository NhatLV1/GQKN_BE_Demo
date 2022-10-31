using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PVI.GQKN.Infrastructure.EntityConfigrurations;

internal class ChucDanhEntityConfiguration : DefaultEntityTypeConfiguration<ChucDanh>
{
    public override void Configure(EntityTypeBuilder<ChucDanh> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tbl_chuc_danh");

        builder.HasKey(x => x.Id);
        builder.HasIndex(e => e.MaChucVu)
            .IsUnique(true);

        builder.Property(e => e.MaChucVu)
            .HasMaxLength(20)
            .IsRequired(true);

        builder.Property(e => e.TenChucVu)
            .HasMaxLength(50)
            .IsRequired(true);

        builder.Ignore(e => e.DomainEvents);
    }
}
