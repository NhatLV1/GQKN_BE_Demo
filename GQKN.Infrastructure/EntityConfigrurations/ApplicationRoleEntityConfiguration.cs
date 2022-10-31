
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PVI.GQKN.Infrastructure.EntityConfigrurations;

internal class ApplicationRoleEntityConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        //builder.HasIndex(e => e.VaiTroId)
        //    .IsUnique(true);

        //builder.Property(e => e.VaiTroId)
        //   .ValueGeneratedOnAdd()
        //   .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        //builder.Property(f => f.VaiTroId)
        //    .ValueGeneratedOnAdd();

        builder.HasOne(e => e.PhongBan)
            .WithMany()
            .HasForeignKey(e => e.PhongBanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.DonVi)
            .WithMany()
            .HasForeignKey(e => e.DonViId)
            .OnDelete(DeleteBehavior.Restrict);
     
        builder.Property(x => x.PhongBanId).IsRequired(false);
        builder.Property(x => x.DonViId).IsRequired(false);

        builder.Property(x => x.NgayTao).HasDefaultValueSql("getdate()"); // SQL Server
        builder.Property(x => x.NgaySua).ValueGeneratedOnAddOrUpdate()
            .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Save);
    }
}
