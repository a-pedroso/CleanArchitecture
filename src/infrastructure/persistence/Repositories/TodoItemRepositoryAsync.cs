using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class TodoItemRepositoryAsync : GenericRepositoryAsync<TodoItem, int>, ITodoItemRepositoryAsync
    {
        private readonly DbSet<TodoItem> _todoItems;

        public TodoItemRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _todoItems = dbContext.Set<TodoItem>();
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsByListIdAsync(int listId)
        {
            return await _todoItems.Where(w => w.ListId == listId)
                                   .ToListAsync();
        }
    }
}
