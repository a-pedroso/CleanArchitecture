using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoLists.Queries.GetTodos
{
    public class GetTodosQuery : IRequest<TodosVm>
    {
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly ITodoListRepositoryAsync _repo;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(ITodoListRepositoryAsync repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var records = await _repo.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<TodoListDto>>(records)
                                .ToList();

            return new TodosVm
            {
                PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                    .Cast<PriorityLevel>()
                    .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                    .ToList(),

                Lists = mapped
            };
        }
    }
}
