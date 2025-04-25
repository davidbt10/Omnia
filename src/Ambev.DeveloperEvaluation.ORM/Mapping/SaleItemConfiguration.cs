using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Quantity)
                   .IsRequired();

            builder.Property(i => i.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(i => i.DiscountPercentage)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(i => i.TotalAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(i => i.IsCancelled)
                   .IsRequired();

            builder.OwnsOne(i => i.Product, pi =>
            {
                pi.Property(p => p.Id)
                  .HasColumnName("ProductId")
                  .IsRequired();
                pi.Property(p => p.Name)
                  .HasColumnName("ProductName")
                  .HasMaxLength(100)
                  .IsRequired();
            });
        }
    }
}
