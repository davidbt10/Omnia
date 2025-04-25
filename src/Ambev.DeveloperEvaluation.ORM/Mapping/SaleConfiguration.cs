using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.SaleNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.Date)
                   .IsRequired();

            builder.OwnsOne(s => s.Customer, ci =>
            {
                ci.Property(c => c.Id).HasColumnName("CustomerId");
                ci.Property(c => c.Name).HasColumnName("CustomerName").HasMaxLength(100);
            });

            builder.OwnsOne(s => s.Branch, bi =>
            {
                bi.Property(b => b.Id).HasColumnName("BranchId");
                bi.Property(b => b.Name).HasColumnName("BranchName").HasMaxLength(100);
            });

            builder.Property(s => s.TotalAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(s => s.IsCancelled)
                   .IsRequired();

            builder.HasMany(s => s.Items)
                   .WithOne()
                   .HasForeignKey("SaleId")
                   .IsRequired();

            builder.Metadata
                   .FindNavigation(nameof(Sale.Items))
                   .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
