using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoItems.Commands.UpdateTodoItem
{
    public partial class UpdateTodoItemCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
    {
        private readonly ITodoItemRepositoryAsync _repo;

        public UpdateTodoItemCommandHandler(ITodoItemRepositoryAsync repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.Title = request.Title;
            entity.Done = request.Done;

            await _repo.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}
