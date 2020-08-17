using CleanArchitecture.Application.Common.Interfaces.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        private readonly ITodoListRepositoryAsync _repo;

        public CreateTodoListCommandValidator(ITodoListRepositoryAsync repo)
        {
            _repo = repo;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _repo.IsUniqueTitleAsync(title);
        }
    }
}
