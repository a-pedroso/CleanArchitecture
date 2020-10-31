using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Context.Configurations
{
    public class TodoItemConfiguration : BaseAuditableEntityConfiguration<TodoItem, int>
    {
        public override void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(p => p.List)
                   .WithMany(p => p.Items)
                   .HasForeignKey(p => p.ListId);
        }
    }
}
