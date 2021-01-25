using FluentValidation;

namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
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
