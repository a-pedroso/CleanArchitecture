using FluentValidation;

namespace CleanArchitecture.Application.Features.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
