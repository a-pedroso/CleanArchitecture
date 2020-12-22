using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.DTO
{
    public record TodosDTO
    {
        public IList<PriorityLevelDTO> PriorityLevels { get; set; }

        public IList<TodoListDTO> Lists { get; set; }
    }
}
