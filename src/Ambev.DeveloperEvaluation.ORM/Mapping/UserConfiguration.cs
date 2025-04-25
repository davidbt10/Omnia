using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).HasMaxLength(20);

        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("Name_FirstName")
                .IsRequired()
                .HasMaxLength(100);

            name.Property(n => n.LastName)
                .HasColumnName("Name_LastName")
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsOne(u => u.Address, addr =>
        {
            addr.Property(a => a.City)
                .HasColumnName("Address_City")
                .IsRequired()
                .HasMaxLength(100);

            addr.Property(a => a.Street)
                .HasColumnName("Address_Street")
                .IsRequired()
                .HasMaxLength(100);

            addr.Property(a => a.Number)
                .HasColumnName("Address_Number");

            addr.Property(a => a.Zipcode)
                .HasColumnName("Address_Zipcode")
                .IsRequired()
                .HasMaxLength(20);

            addr.OwnsOne(a => a.Geolocation, geo =>
            {
                geo.Property(g => g.Lat)
                    .HasColumnName("Address_Geolocation_Lat")
                    .IsRequired()
                    .HasMaxLength(50);

                geo.Property(g => g.Long)
                    .HasColumnName("Address_Geolocation_Long")
                    .IsRequired()
                    .HasMaxLength(50);
            });
        });
    }
}
