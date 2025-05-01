using ProductCatalog.Products.Features.CreateProduct;
using ProductCatalog.Products.Features.GetProducts;
using ProductCatalog.Products.Infrastructure.Mappings;

namespace ProductCatalog.Tests.Products.Infrastructure.Mappings
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class MappingProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void Mapping_Configuration_ShouldBeValid()
        {
            // Arrange & Act
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            // Assert
            config.AssertConfigurationIsValid();
        }

        [Test]
        public void Should_Map_CreateProductRequest_To_CreateProductCommand()
        {
            // Arrange
            var request = new CreateProductRequest(
                "Code",
                "Test Product",
                new List<string> { "Electronics" },
                "Computer",
                "computer.jpg",
                50
            );

            // Act
            var command = _mapper.Map<CreateProductCommand>(request);

            // Assert
            command.Name.Should().Be(request.Name);
            command.Code.Should().Be(request.Code);
            command.Category.Should().BeEquivalentTo(request.Category);
            command.Description.Should().Be(request.Description);
            command.ImageFile.Should().Be(request.ImageFile);
            command.Price.Should().Be(request.Price);
        }

        [Test]
        public void Should_Map_CreateProductCommand_To_Product_And_Ignore_Id()
        {
            // Arrange
            var command = new CreateProductCommand(
                "Code",
                "Test Product",
                new List<string> { "Electronics" },
                "Computer",
                "computer.jpg",
                50
            );

            // Act
            var product = _mapper.Map<Product>(command);

            // Assert
            product.Name.Should().Be(command.Name);
            product.Code.Should().Be(command.Code);
            product.Category.Should().BeEquivalentTo(command.Category);
            product.Description.Should().Be(command.Description);
            product.ImageFile.Should().Be(command.ImageFile);
            product.Price.Should().Be(command.Price);
            product.Id.Should().BeEmpty();
        }

        [Test]
        public void Should_Map_GetProductsRequest_To_GetProductsQuery()
        {
            // Arrange
            var request = new GetProductsRequest(PageNumber: 1, PageSize: 10);

            // Act
            var query = _mapper.Map<GetProductsQuery>(request);

            // Assert
            query.PageNumber.Should().Be(request.PageNumber);
            query.PageSize.Should().Be(request.PageSize);
        }

    }
}
