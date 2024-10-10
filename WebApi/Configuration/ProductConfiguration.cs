/*using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.CategoryId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Stock)
            .IsRequired();

        builder.Property(p => p.Tags)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            )
            .HasMaxLength(1000);

        builder.Property(p => p.Weight)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

   
        builder.Property(d => d.Height)
            .HasColumnType("decimal(18, 2)");
        
        builder.Property(d => d.Width).
            HasColumnType("decimal(18, 2)");
        
        builder.Property(d => d.Depth).
            HasColumnType("decimal(18, 2)");
        
        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Property(p => p.UpdatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasMany(p => p.Reviews)
            .WithOne()
            .HasForeignKey(r => r.ProductId);

        builder.Property(p => p.QrCode)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.BarCode)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.Images)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            )
            .HasMaxLength(2000);

        builder.Property(p => p.Thumbnail)
            .IsRequired(false)
            .HasMaxLength(500);
    }

}*/


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Table Name
        builder.ToTable("Products");

        // Primary Key
        builder.HasKey(p => p.Id);

        // Properties
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(100); // Assuming a max length of 100 for title
        
        builder.Property(p => p.Description)
            .HasMaxLength(500); // Assuming a max length of 500 for description
        
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(p => p.Stock)
            .IsRequired();
        
        builder.Property(p => p.Weight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(p => p.Width)
            .HasColumnType("decimal(18,2)");
        
        builder.Property(p => p.Depth)
            .HasColumnType("decimal(18,2)");
        
        builder.Property(p => p.Height)
            .HasColumnType("decimal(18,2)");

        // Date Fields
        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // BarCode & QrCode
        builder.Property(p => p.BarCode)
            .HasMaxLength(255); // Assuming a max length of 50 for barcode
        
        builder.Property(p => p.QrCode)
            .HasMaxLength(255); // Assuming a max length of 50 for QR code

        // Thumbnail
        builder.Property(p => p.Thumbnail)
            .HasMaxLength(200); // Assuming a max length of 200 for thumbnail URL

        // Collections
        builder.Property(p => p.Tags)
            .HasConversion(
                tags => string.Join(",", tags),
                tags => tags.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .HasMaxLength(250); // Assuming the maximum combined length of tags is 250

        builder.Property(p => p.Images)
            .HasConversion(
                images => string.Join(",", images),
                images => images.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .HasMaxLength(1000); // Assuming a combined max length for images is 1000

        // Relationships
        builder.Property(p => p.CategoryId)
            .IsRequired()
            .HasMaxLength(255);
        
        // Indexes
        builder.HasIndex(p => p.BarCode).IsUnique();
        builder.HasIndex(p => p.QrCode).IsUnique();
        
        builder.HasOne(a => a.User)
            .WithMany() // Adjust this based on your desired inverse navigation property in User
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
