using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanArchitecture.Infrastructure.Persistence.Context.Configurations
{
    public class BaseAuditableEntityConfiguration<T, TKey> : IEntityTypeConfiguration<T>
        where TKey : IEquatable<TKey>
        where T : BaseAuditableEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // IDENTITY
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            // AUDIT
            builder.Property(t => t.Created)
                .IsRequired();

            builder.Property(t => t.CreatedBy)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.LastModifiedBy)
                .HasMaxLength(255);
        }
    }
}
