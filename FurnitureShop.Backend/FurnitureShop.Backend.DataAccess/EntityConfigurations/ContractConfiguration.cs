using FurnitureShop.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureShop.Backend.DataAccess.EntityConfigurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contract");
        builder.HasKey(c => c.Number);

        builder.Property(c => c.Number).ValueGeneratedOnAdd().IsRequired();
        builder.Property(c => c.BuyerCode).IsRequired();
        builder.Property(c => c.IssueDate).IsRequired();
        builder.Property(c => c.DueDate).IsRequired();

        builder
            .HasOne(c => c.Buyer)
            .WithMany(b => b.Contracts)
            .HasForeignKey(c => c.BuyerCode)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}