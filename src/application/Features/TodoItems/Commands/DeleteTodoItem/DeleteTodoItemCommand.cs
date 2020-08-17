using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly ITodoItemRepositoryAsync _repo;

        public DeleteTodoItemCommandHandler(ITodoItemRepositoryAsync repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            await _repo.DeleteAsync(entity);

            return Unit.Value;
        }
    }
}
