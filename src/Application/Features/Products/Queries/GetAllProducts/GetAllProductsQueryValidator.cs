namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    using FluentValidation;

    public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
    {
        public GetAllProductsQueryValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} has to be greater than zero.");

            RuleFor(p => p.PageSize)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} has to be greater than zero.");
        }
    }
}
