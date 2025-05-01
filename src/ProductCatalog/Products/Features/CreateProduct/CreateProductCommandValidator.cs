namespace ProductCatalog.Products.Features.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code should not be empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empty");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category should not be empty");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile should not be empty");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
