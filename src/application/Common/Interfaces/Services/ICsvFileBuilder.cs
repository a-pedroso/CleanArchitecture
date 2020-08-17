using CleanArchitecture.Application.Features.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
