using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVI.GQKN.Infrastructure.EntityConfigrurations
{
    public class TaiLieuBookMarkEntityTypeConfiguration : DefaultEntityTypeConfiguration<TaiLieuBookMark>
    {
        public override void Configure(EntityTypeBuilder<TaiLieuBookMark> builder)
        {
            base.Configure(builder);
            builder.ToTable("tailieu_bookmark");

            builder.Property(x => x.TenBookMark)
          .HasMaxLength(50);

            builder.Property(x => x.DuongDanBookMark)
          .HasMaxLength(150);

            builder.Property(x => x.MaBookMark)
         .HasMaxLength(50);

            builder.HasOne(e => e.TaiLieu)
            .WithMany()
            .HasForeignKey(x => x.TaiLieuId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
