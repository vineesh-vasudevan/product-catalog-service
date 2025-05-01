namespace ProductCatalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(string Code, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResponse>;
}
