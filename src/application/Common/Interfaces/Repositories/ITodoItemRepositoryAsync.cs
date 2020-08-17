using CleanArchitecture.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces.Repositories
{
    public interface ITodoItemRepositoryAsync : IGenericRepositoryAsync<TodoItem, int>
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsByListIdAsync(int listId);
    }
}
