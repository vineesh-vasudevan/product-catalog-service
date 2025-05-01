namespace ProductCatalog.Products.Features.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResponse>;
}
