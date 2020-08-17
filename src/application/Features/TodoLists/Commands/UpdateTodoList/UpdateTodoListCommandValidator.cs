using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
    {
        private readonly ITodoListRepositoryAsync _repo;

        public UpdateTodoListCommandValidator(ITodoListRepositoryAsync repo)
        {
            _repo = repo;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken)
        {
            return await _repo.IsUniqueTitleAsync(title);
        }
    }
}
