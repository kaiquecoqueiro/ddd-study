using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Infrastructure.Db.Ef.Model;

namespace src.Infrastructure.Db.Ef.Mappings
{
    public class OrderModelMapping : IEntityTypeConfiguration<OrderModel>
    {
        public void Configure(EntityTypeBuilder<OrderModel> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            // builder.Property(x => x.CustomerId)
            //     .HasColumnType("varchar(20)");

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);
        }
    }
}