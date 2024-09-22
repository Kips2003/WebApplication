using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Configure the primary key
        builder.HasKey(o => o.Id);

        // Configure the properties
        builder.Property(o => o.OrderDate)
            .IsRequired();

        builder.Property(o => o.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed

        // Configure the relationship with User
        builder.HasOne(o => o.User)
            .WithMany() // Adjust this based on your desired inverse navigation property in User
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure the relationship with OrderItem
        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}