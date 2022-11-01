using FurnitureShop.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureShop.Backend.DataAccess.EntityConfigurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");
        builder.HasKey(b => new { b.ContractNumber, b.FurnitureModel});

        builder.Property(s => s.Count).IsRequired();

        builder
            .HasOne(s => s.Contract)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.ContractNumber)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder
            .HasOne(s => s.Furniture)
            .WithMany(f => f.Sales)
            .HasForeignKey(s => s.FurnitureModel)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}