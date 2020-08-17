using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class TodoListRepositoryAsync : GenericRepositoryAsync<TodoList, int>, ITodoListRepositoryAsync
    {
        private readonly DbSet<TodoList> _todoList;

        public TodoListRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _todoList = dbContext.Set<TodoList>();
        }

        public async Task<bool> IsUniqueTitleAsync(string title)
        {
            return await _todoList.AllAsync(p => p.Title != title);
        }
    }
}
