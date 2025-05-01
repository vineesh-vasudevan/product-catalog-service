using ProductCatalog.Products.Infrastructure.Repositories;

namespace ProductCatalog.Products.Features.GetProductById
{
    public class GetProductByIdQueryHandler(IProductRepository productRepository)
        : IQueryHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
    {
        public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(query.Id, cancellationToken);

            return product is not null
                ? Result.Success(new GetProductByIdResponse(product))
                : Result.Failure<GetProductByIdResponse>($"Product with ID {query.Id} was not found.");
        }
    }
}
