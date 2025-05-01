using ProductCatalog.Products.Features.GetProductById;
using ProductCatalog.Products.Infrastructure.Repositories;

namespace ProductCatalog.Tests.Products.Features.GetProductById
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class GetProductByIdQueryHandlerTests
    {
        [Test]
        public async Task Handle_ShouldReturnSuccessResult_WhenProductExists()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            var query = new GetProductByIdQuery(productId);
            var product = new Product { Id = productId, Name = "Test Product" };

            var repository = Substitute.For<IProductRepository>();
            repository.GetById(productId, Arg.Any<CancellationToken>())
                       .Returns(product);
            var handler = new GetProductByIdQueryHandler(repository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Product.Should().Be(product);
        }

        [Test]
        public async Task Handle_ShouldReturnFailureResult_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var query = new GetProductByIdQuery(productId);
            var repository = Substitute.For<IProductRepository>();
            repository.GetById(productId, Arg.Any<CancellationToken>())
                       .Returns((Product?)null);
            var handler = new GetProductByIdQueryHandler(repository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be($"Product with ID {productId} was not found.");
        }
    }
}
