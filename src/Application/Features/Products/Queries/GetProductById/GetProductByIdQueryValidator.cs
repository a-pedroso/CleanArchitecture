using FluentValidation;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} has to be greater than zero.");
        }
    }
}
