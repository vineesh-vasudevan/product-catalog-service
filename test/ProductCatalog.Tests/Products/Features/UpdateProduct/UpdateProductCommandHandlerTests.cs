using ProductCatalog.Products.Features.UpdateProduct;
using ProductCatalog.Products.Infrastructure.Repositories;

namespace ProductCatalog.Tests.Products.Features.UpdateProduct
{
    [TestFixture]
    public class UpdateProductCommandHandlerTests
    {
        [Test]
        public async Task Handle_ShouldUpdateProduct_WhenProductExists()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var handler = new UpdateProductCommandHandler(repository);
            var productId = Guid.NewGuid();
            var existingProduct = new Product
            {
                Id = productId,
                Code = "Code",
                Name = "Old Name",
                Category = ["Old Category"],
                Description = "Old Description",
                ImageFile = "old.png",
                Price = 10.0M
            };

            repository.GetById(productId, Arg.Any<CancellationToken>())
                       .Returns(existingProduct);

            var command = new UpdateProductCommand(productId, "Code", "New Name", ["New Category"], "New Description", "new.png", 20.5M);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOfType<UpdateProductResponse>();

            var updated = result.Value.Product;
            updated.Code.Should().Be("Code");
            updated.Name.Should().Be("New Name");
            updated.Category[0].Should().Be("New Category");
            updated.Description.Should().Be("New Description");
            updated.ImageFile.Should().Be("new.png");
            updated.Price.Should().Be(20.5M);

            await repository.Received(1).Update(existingProduct, Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenProductNotFound()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var handler = new UpdateProductCommandHandler(repository);
            var productId = Guid.NewGuid();
            repository.GetById(productId, Arg.Any<CancellationToken>())
                       .Returns((Product)null!);

            var command = new UpdateProductCommand(productId, "Code", "New Name", ["New Category"], "New Description", "new.png", 20.5M);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be($"Product with ID {productId} was not found.");
            await repository.DidNotReceive().Update(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }
    }
}
