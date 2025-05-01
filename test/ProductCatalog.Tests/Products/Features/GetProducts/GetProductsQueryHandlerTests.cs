using ProductCatalog.Products.Features.GetProducts;
using ProductCatalog.Products.Infrastructure.Repositories;

namespace ProductCatalog.Tests.Products.Features.GetProducts
{
    [TestFixture]
    public class GetProductsQueryHandlerTests
    {
        [Test]
        public async Task Should_Return_First_Page_With_Default_PageSize()
        {
            // Arrange
            var products = CreateMockProducts();
            var repository = Substitute.For<IProductRepository>();
            var query = new GetProductsQuery { PageNumber = 1, PageSize = 10 };
            var handler = new GetProductsQueryHandler(repository);
            var cancellationToken = CancellationToken.None;

            repository
                .GetProducts(1, 10, cancellationToken)
                .Returns(products);

            // Act
            var response = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Products.Count, Is.EqualTo(products.Count));
            await repository.Received(1).GetProducts(1, 10, cancellationToken);
        }

        private static List<Product> CreateMockProducts() =>
        [
            new Product { Id = Guid.NewGuid(), Name = "Mouse", Price = 20 },
            new Product { Id = Guid.NewGuid(), Name = "Keyboard", Price = 30 },
            new Product { Id = Guid.NewGuid(), Name = "Monitor", Price = 150 },
            new Product { Id = Guid.NewGuid(), Name = "USB Hub", Price = 25 },
            new Product { Id = Guid.NewGuid(), Name = "Webcam", Price = 50 }
        ];
    }
}
