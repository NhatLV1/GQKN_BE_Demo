using Microsoft.EntityFrameworkCore.Metadata;

namespace PVI.GQKN.Infrastructure.EntityConfigrurations;

public abstract class DefaultEntityTypeConfiguration<T> :
    IEntityTypeConfiguration<T>
    where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Guid)
        .IsUnique();

        builder.Property(x => x.Guid).HasDefaultValueSql("NEWID()");
        //builder.Property(x => x.Guid)
        //    .ValueGeneratedOnAdd();

        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.NgayTao).HasDefaultValueSql("getdate()"); // SQL Server
        builder.Property(x => x.NgaySua)
            .ValueGeneratedOnAddOrUpdate()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
      

        builder.Ignore(e => e.DomainEvents);

    }
}
