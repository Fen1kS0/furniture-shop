using FurnitureShop.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureShop.Backend.DataAccess.EntityConfigurations;

public class FurnitureConfiguration : IEntityTypeConfiguration<Furniture>
{
    public void Configure(EntityTypeBuilder<Furniture> builder)
    {
        builder.ToTable("Furniture");
        builder.HasKey(f => f.Model);

        builder.Property(f => f.Model).ValueGeneratedOnAdd().IsRequired();
        builder.Property(f => f.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(f => f.Price).IsRequired();
        builder.Property(f => f.Characteristics).IsRequired(false);
    }
}