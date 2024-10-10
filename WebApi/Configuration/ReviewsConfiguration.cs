using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configuration;

public class ReviewsConfiguration : IEntityTypeConfiguration<Reviews>
{
    public void Configure(EntityTypeBuilder<Reviews> builder)
    {
// Table name configuration
        builder.ToTable("Reviews");

        // Primary key
        builder.HasKey(r => r.Id);

        // Foreign key configuration with User entity
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Foreign key configuration with Product entity
        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Property configurations
        builder.Property(r => r.Comment)
            .IsRequired()
            .HasMaxLength(1000); // Optional: Adjust max length based on your requirements

        builder.Property(r => r.Rating)
            .IsRequired();

        // Optional: Configure additional properties if needed
        builder.Property(r => r.Images)
            .HasConversion(
                v => string.Join(',', v),  // Convert string array to a single string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)) // Convert string back to array
            .HasColumnType("text") // Store as a long text value
            .IsRequired(false);
    }
}