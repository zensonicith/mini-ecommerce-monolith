using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(o => o.Currency)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(o => o.Status)
            .IsRequired();

        builder.Property(o => o.StripePaymentIntentId)
            .HasMaxLength(200);

        builder.Property(o => o.OrderEmail)
            .HasMaxLength(200);

        builder.Property(o => o.ShippingName)
            .HasMaxLength(200);

        builder.Property(o => o.ShippingPhone)
            .HasMaxLength(50);

        builder.Property(o => o.ShippingAddressLine)
            .HasMaxLength(500);

        builder.Property(o => o.ShippingCity)
            .HasMaxLength(100);

        builder.Property(o => o.ShippingCountry)
            .HasMaxLength(100);

        builder.Property(o => o.ShippingPostalCode)
            .HasMaxLength(20);

        builder.Property(o => o.CreatedAt)
            .IsRequired();
    }
}