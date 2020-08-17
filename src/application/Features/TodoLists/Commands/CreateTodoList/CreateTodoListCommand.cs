using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoLists.Commands.CreateTodoList
{
    public partial class CreateTodoListCommand : IRequest<int>
    {
        public string Title { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
    {
        private readonly ITodoListRepositoryAsync _repo;

        public CreateTodoListCommandHandler(ITodoListRepositoryAsync repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoList
            {
                Title = request.Title
            };

            await _repo.AddAsync(entity);

            return entity.Id;
        }
    }
}
