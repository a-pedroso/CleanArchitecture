using FluentValidation;

namespace CleanArchitecture.Application.Features.Products.Commands.DeleteProductById
{
    public class DeleteProductByIdCommandValidator : AbstractValidator<DeleteProductByIdCommand>
    {
        public DeleteProductByIdCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}
