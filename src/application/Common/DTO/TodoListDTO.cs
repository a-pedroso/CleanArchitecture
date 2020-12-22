using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.DTO
{
    public record TodoListDTO
    {
        public TodoListDTO()
        {
            Items = new List<TodoItemDTO>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public IList<TodoItemDTO> Items { get; set; }
    }
}
