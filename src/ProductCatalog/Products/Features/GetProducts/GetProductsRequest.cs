namespace ProductCatalog.Products.Features.GetProducts
{
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
}
