namespace CleanArchitecture.Application.Features.Products.Commands.CreateProduct
{
    using FluentValidation;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepository productRepository;

        public CreateProductCommandValidator(IProductRepository productRepository)
        {
            this.productRepository = productRepository;

            RuleFor(p => p.Barcode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.")
                .MustAsync(IsUniqueBarcode).WithMessage("{PropertyName} already exists.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(2000).WithMessage("{PropertyName} must not exceed 2000 characters.");

            RuleFor(p => p.Rate)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater or equal to zero.");
        }

        private async Task<bool> IsUniqueBarcode(string barcode, CancellationToken cancellationToken)
        {
            return await productRepository.IsUniqueBarcodeAsync(barcode);
        }
    }
}
