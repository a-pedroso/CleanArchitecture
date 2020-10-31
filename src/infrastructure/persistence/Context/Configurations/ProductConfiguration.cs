using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Context.Configurations
{
    public class ProductConfiguration : BaseAuditableEntityConfiguration<Product, long>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Barcode)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(t => t.Rate)
                .HasDefaultValue(0)
                .IsRequired();
        }
    }
}
