using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);

        // Use TIMESTAMP instead of DATETIME or remove default value if not needed
        builder.Property(u => u.CreatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");


        builder.HasMany(u => u.Reviews)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId);
        /*builder.Property(u => u.IsEmailConfirmed)
            .IsRequired() // Ensures this field cannot be null
            .HasDefaultValue(false); */
        builder.Property(u => u.ProfilePicture)
            .HasMaxLength(200); 
        
        builder.HasMany(u => u.Products)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

        builder.HasMany(u => u.CartItems)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        builder.HasMany(u => u.Reviews)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId);

        builder.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);

    }
}