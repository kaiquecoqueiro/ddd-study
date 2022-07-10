using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Infrastructure.Db.Ef.Model;

namespace src.Infrastructure.Db.Ef.Mappings
{
    public class OrderItemModelMapping : IEntityTypeConfiguration<OrderItemModel>
    {
        public void Configure(EntityTypeBuilder<OrderItemModel> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(20)");

            builder.Property(x => x.Price)
                .HasColumnType("int");

            builder.Property(x => x.ProductId)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.Quantity)
                .HasColumnType("int");
        }
    }
}