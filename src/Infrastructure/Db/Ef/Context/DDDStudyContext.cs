using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Db.Ef.Mappings;
using src.Infrastructure.Db.Ef.Model;

namespace src.Infrastructure.Db.Ef.Context
{
    public class DDDStudyContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<OrderModel> Orders { get; set; }

        public DDDStudyContext(DbContextOptions<DDDStudyContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductModelMapping());
            modelBuilder.ApplyConfiguration(new CustomerModelMapping());
            modelBuilder.ApplyConfiguration(new OrderModelMapping());
            modelBuilder.ApplyConfiguration(new OrderItemModelMapping());
        }
    }
}