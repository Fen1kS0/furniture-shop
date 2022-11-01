using FurnitureShop.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureShop.Backend.DataAccess.EntityConfigurations;

public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.ToTable("Buyer");
        builder.HasKey(b => b.Code);

        builder.Property(b => b.Code).ValueGeneratedOnAdd().IsRequired();
        builder.Property(b => b.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(b => b.Address)
            .HasMaxLength(250)
            .IsRequired();
        builder.Property(b => b.NumberPhone)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(b => b.NumberPhone).IsUnique();
    }
}