using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVI.GQKN.Infrastructure.EntityConfigrurations
{
    public class ThuMucEntityTypeConfiguration : DefaultEntityTypeConfiguration<ThuMuc>
    {
        public override void Configure(EntityTypeBuilder<ThuMuc> builder)
        {
            base.Configure(builder);
            builder.ToTable("thu_muc");

            builder.HasOne(e => e.HoSoTonThat)
            .WithMany()
            .HasForeignKey(x => x.HoSoTonThatId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
