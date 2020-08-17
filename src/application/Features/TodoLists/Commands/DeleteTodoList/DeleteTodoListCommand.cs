using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoLists.Commands.DeleteTodoList
{
    public class DeleteTodoListCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
    {
        private readonly ITodoListRepositoryAsync _repo;

        public DeleteTodoListCommandHandler(ITodoListRepositoryAsync repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.Id);
            }

            await _repo.DeleteAsync(entity);

            return Unit.Value;
        }
    }
}
