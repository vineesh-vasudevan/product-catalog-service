using ProductCatalog.Products.Features.CreateProduct;
using ProductCatalog.Products.Infrastructure.Repositories;

namespace ProductCatalog.Tests.Products.Features.CreateProduct
{
    [TestFixture]
    public class CreateProductCommandHandlerTests
    {
        [Test]
        public async Task Handle_Should_Map_Store_Save_And_Return_Response()
        {
            //Arrange
            var repository = Substitute.For<IProductRepository>();
            var mapper = Substitute.For<IMapper>();
            var sut = new CreateProductCommandHandler(repository, mapper);
            var cancellationToken = CancellationToken.None;
            var productId = Guid.NewGuid();

            var command = new CreateProductCommand("Test Product", ["Electronics"], "Computer", "computer.jpg", 50);

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            mapper.Map<Product>(command).Returns(product);

            repository.Add(product, cancellationToken).Returns(productId);

            // Act
            var result = await sut.Handle(command, cancellationToken);

            // Assert
            await repository.Received(1).Add(product, cancellationToken);
            result.Should().NotBeNull();
            result.Id.Should().Be(productId);
        }
    }
}
