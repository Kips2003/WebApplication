using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        // Configure the primary key
        builder.HasKey(oi => oi.Id);

        // Configure the Order foreign key relationship
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems) // Order has many OrderItems
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure the Product foreign key relationship
        builder.HasOne(oi => oi.Product)
            .WithMany() // Product may have many OrderItems, adjust if Product has a collection of OrderItems
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // Change this behavior as needed

        // Configure properties
        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.Property(oi => oi.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed
    }

}