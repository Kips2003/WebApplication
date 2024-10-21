using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configuration;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        // Configure the primary key
        builder.HasKey(ci => ci.Id);

        // Configure the Cart foreign key relationship
        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems) // Cart has many CartItems
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure the Product foreign key relationship
        builder.HasOne(ci => ci.Product)
            .WithMany() // Product may have many CartItems, adjust if Product has a collection of CartItems
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // Change this behavior as needed

        // Configure properties
        builder.Property(ci => ci.Quantity)
            .IsRequired();
        
    }

}