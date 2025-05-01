using ProductCatalog.Products.Features.DeleteProduct;
using ProductCatalog.Products.Infrastructure.Repositories;

namespace ProductCatalog.Tests.Products.Features.DeleteProduct
{
    [TestFixture]
    public class DeleteProductCommandHandlerTests
    {
        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenProductExists()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var handler = new DeleteProductCommandHandler(repository);
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };
            repository
                .GetById(productId, Arg.Any<CancellationToken>())
                .Returns(product);

            var command = new DeleteProductCommand(productId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
            await repository.Received(1).Delete(productId, Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenProductDoesNotExist()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var handler = new DeleteProductCommandHandler(repository);
            var productId = Guid.NewGuid();
            repository
                .GetById(productId, Arg.Any<CancellationToken>())
                .Returns((Product)null!);

            var command = new DeleteProductCommand(productId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be($"Product with ID {productId} was not found.");
            await repository.DidNotReceive().Delete(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }

    }
}
