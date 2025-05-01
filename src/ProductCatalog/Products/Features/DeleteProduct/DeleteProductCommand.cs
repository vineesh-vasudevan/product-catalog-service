namespace ProductCatalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<Result<bool>>;
}
