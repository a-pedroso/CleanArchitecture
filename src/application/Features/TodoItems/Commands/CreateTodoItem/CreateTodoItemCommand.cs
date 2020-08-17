using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public int ListId { get; set; }

        public string Title { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly ITodoItemRepositoryAsync _repo;

        public CreateTodoItemCommandHandler(ITodoItemRepositoryAsync repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                ListId = request.ListId,
                Title = request.Title,
                Done = false
            };

            await _repo.AddAsync(entity);

            return entity.Id;
        }
    }
}
