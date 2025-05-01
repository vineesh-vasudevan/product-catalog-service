namespace ProductCatalog.Products.Features.CreateProduct
{
    public record CreateProductRequest(string Code, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
}
