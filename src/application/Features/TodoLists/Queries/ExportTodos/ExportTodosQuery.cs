using AutoMapper;
using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Interfaces.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoLists.Queries.ExportTodos
{
    public class ExportTodosQuery : IRequest<ExportTodosDTO>
    {
        public int ListId { get; set; }
    }

    public class ExportTodosQueryHandler : IRequestHandler<ExportTodosQuery, ExportTodosDTO>
    {
        private readonly ITodoItemRepositoryAsync _repo;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportTodosQueryHandler(ITodoItemRepositoryAsync repo, IMapper mapper, ICsvFileBuilder fileBuilder)
        {
            _repo = repo;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
        }

        public async Task<ExportTodosDTO> Handle(ExportTodosQuery request, CancellationToken cancellationToken)
        {
            var vm = new ExportTodosDTO();

            var records = await _repo.GetTodoItemsByListIdAsync(request.ListId);

            IEnumerable<ExportTodoItemFileRecordDTO> result = _mapper.Map<IEnumerable<ExportTodoItemFileRecordDTO>>(records);

            vm.Content = _fileBuilder.BuildTodoItemsFile(result);
            vm.ContentType = "text/csv";
            vm.FileName = "TodoItems.csv";

            return await Task.FromResult(vm);
        }
    }
}
