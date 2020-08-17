using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }

    public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
    {
        private readonly ITodoListRepositoryAsync _repo;

        public UpdateTodoListCommandHandler(ITodoListRepositoryAsync repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.Id);
            }

            entity.Title = request.Title;

            await _repo.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}
