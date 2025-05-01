using ProductCatalog.Products.Infrastructure.Repositories;

namespace ProductCatalog.Products.Features.GetProducts
{
    public class GetProductsQueryHandler(IProductRepository repository)
        : IQueryHandler<GetProductsQuery, GetProductsResponse>
    {
        public async Task<GetProductsResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await repository.GetProducts(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            return new GetProductsResponse(products);
        }
    }
}
