namespace ProductCatalog.Products.Features.UpdateProduct
{
    public record UpdateProductRequest(Guid? Id,string Code, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
}
