using ProductCatalog.Models;
namespace ProductCatalog.Products.Features.GetProducts
{
    public record GetProductsResponse(IEnumerable<Product> Products);
}
