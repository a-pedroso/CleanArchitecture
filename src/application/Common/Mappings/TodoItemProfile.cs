using AutoMapper;
using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Mappings
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));

            CreateMap<TodoItem, ExportTodoItemFileRecordDTO>();
        }
    }
}
