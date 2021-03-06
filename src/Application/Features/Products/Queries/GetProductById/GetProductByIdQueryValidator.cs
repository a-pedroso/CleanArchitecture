namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    using FluentValidation;

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
