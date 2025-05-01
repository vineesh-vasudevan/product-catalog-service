using Marten.Pagination;
using ProductCatalog.Models;

namespace ProductCatalog.Products.Infrastructure.Repositories
{
    public class ProductRepository(IDocumentSession session) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetProducts(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .ToPagedListAsync(pageNumber, pageSize, cancellationToken);

            return products;
        }

        public async Task<Guid> Add(Product product, CancellationToken cancellationToken)
        {
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return product.Id;
        }

        public async Task<Product?> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await session.LoadAsync<Product>(id, cancellationToken);
        }

        public async Task Update(Product product, CancellationToken cancellationToken)
        {
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            session.Delete<Product>(id);
            await session.SaveChangesAsync(cancellationToken);
        }
    }
}
