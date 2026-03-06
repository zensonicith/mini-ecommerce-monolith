using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProducts>
    {
        public void Configure(EntityTypeBuilder<OrderProducts> builder)
        {
            builder.ToTable("OrderProducts");

            builder.HasKey(op => new { op.OrderId, op.ProductId });

            builder.HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            builder.HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);
        }
    }
}
