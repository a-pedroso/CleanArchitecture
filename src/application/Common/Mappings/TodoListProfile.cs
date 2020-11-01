using AutoMapper;
using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Mappings
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<TodoList, TodoListDTO>();
        }
    }
}
