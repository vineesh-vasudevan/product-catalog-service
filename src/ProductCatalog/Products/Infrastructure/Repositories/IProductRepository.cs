using ProductCatalog.Models;
namespace ProductCatalog.Products.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<Guid> Add(Product product, CancellationToken cancellationToken);

        Task<Product?> GetById(Guid id, CancellationToken cancellationToken);

        Task Update(Product product, CancellationToken cancellationToken);

        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
