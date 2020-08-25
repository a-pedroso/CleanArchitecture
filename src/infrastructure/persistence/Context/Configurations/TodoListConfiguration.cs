using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Context.Configurations
{
    public class TodoListConfiguration :  BaseAuditableEntityConfiguration<TodoList, int>
    {
        public override void Configure(EntityTypeBuilder<TodoList> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
