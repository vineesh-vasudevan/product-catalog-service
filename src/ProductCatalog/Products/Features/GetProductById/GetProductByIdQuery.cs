namespace ProductCatalog.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<Result<GetProductByIdResponse>>;
}
