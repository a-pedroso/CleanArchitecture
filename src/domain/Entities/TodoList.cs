using CleanArchitecture.Domain.Common;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public class TodoList : BaseAuditableEntity<int>
    {
        public TodoList()
        {
            Items = new List<TodoItem>();
        }

        public override int Id { get; set; }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TodoItem> Items { get; set; }
    }
}
