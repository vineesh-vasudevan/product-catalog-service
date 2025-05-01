namespace ProductCatalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Code, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<Result<UpdateProductResponse>>;
}
