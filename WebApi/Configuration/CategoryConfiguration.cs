using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Configure the primary key
        builder.HasKey(c => c.Id);

        // Configure the properties
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
    }

}