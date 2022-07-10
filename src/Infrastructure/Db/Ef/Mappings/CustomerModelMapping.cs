using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Infrastructure.Db.Ef.Model;

namespace src.Infrastructure.Db.Ef.Mappings
{
    public class CustomerModelMapping : IEntityTypeConfiguration<CustomerModel>
    {
        public void Configure(EntityTypeBuilder<CustomerModel> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(20)");

            builder.Property(x => x.Street)
                .HasColumnType("varchar(50)");

            builder.Property(x => x.Number)
                .HasColumnType("int");

            builder.Property(x => x.Zip)
                .HasColumnType("varchar(10)");

            builder.Property(x => x.City)
                .HasColumnType("varchar(10)");

            builder.Property(x => x.IsActive)
                .HasColumnType("bit");

            builder.Property(x => x.RewardPoints)
                .HasColumnType("int");
        }
    }
}