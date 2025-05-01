using ProductCatalog.Models;
using ProductCatalog.Products.Infrastructure.Repositories;

namespace ProductCatalog.Products.Features.CreateProduct
{
    public class CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
        : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(command);
            var productId = await repository.Add(product, cancellationToken);
            return new CreateProductResponse(productId);
        }
    }
}
