using CleanArchitecture.Domain.Entities;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces.Repositories
{
    public interface ITodoListRepositoryAsync : IGenericRepositoryAsync<TodoList, int>
    {
        Task<bool> IsUniqueTitleAsync(string title);
    }
}
