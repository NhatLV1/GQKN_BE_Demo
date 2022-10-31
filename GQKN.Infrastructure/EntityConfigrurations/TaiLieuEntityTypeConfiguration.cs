using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVI.GQKN.Infrastructure.EntityConfigrurations
{
    public class TaiLieuEntityTypeConfiguration : DefaultEntityTypeConfiguration<TaiLieu>
    {
        public override void Configure(EntityTypeBuilder<TaiLieu> builder)
        {
            base.Configure(builder);
            builder.ToTable("dm_tailieu");

            builder.Property(x => x.TenTaiLieu)
           .HasMaxLength(50);

            builder.Property(x => x.DuongDan)
          .HasMaxLength(150);

            builder.HasOne(e => e.HoSoTonThat)
            .WithMany()
            .HasForeignKey(x => x.HoSoTonThatId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ThuMuc)
            .WithMany()
            .HasForeignKey(x => x.ThuMucId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
