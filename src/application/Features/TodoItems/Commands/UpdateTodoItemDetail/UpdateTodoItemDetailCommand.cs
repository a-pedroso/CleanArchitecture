using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoItems.Commands.UpdateTodoItemDetail
{
    public class UpdateTodoItemDetailCommand : IRequest
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public PriorityLevel Priority { get; set; }

        public string Note { get; set; }
    }

    public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
    {
        private readonly ITodoItemRepositoryAsync _repo;

        public UpdateTodoItemDetailCommandHandler(ITodoItemRepositoryAsync repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.ListId = request.ListId;
            entity.Priority = request.Priority;
            entity.Note = request.Note;

            await _repo.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}
