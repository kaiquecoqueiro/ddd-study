using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Infrastructure.Db.Ef.Model;

namespace src.Infrastructure.Db.Ef.Mappings
{
    public class ProductModelMapping : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(20)");

            builder.Property(x => x.Price)
                .HasColumnType("int");
        }
    }
}