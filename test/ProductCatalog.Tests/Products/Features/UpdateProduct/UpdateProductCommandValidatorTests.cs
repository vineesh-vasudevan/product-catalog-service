
using ProductCatalog.Products.Features.UpdateProduct;

namespace ProductCatalog.Tests.Products.Features.UpdateProduct
{
    [TestFixture]
    public class UpdateProductCommandValidatorTests
    {
        private UpdateProductCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UpdateProductCommandValidator();
        }

        [Test]
        public void Should_Have_Error_When_Name_Is_Null()
        {
            //Arrange
            var command = new UpdateProductCommand(Guid.NewGuid(), "Code", null!, [], "Description", "img.jpg", 10);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            //Arrange
            var command = new UpdateProductCommand(Guid.NewGuid(), "Code", "", new List<string>(), "Description", "img.jpg", 10);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Should_Have_Error_When_Name_Is_Too_Short()
        {
            //Arrange
            var command = new UpdateProductCommand(Guid.NewGuid(), "Code", "A", [], "Description", "img.jpg", 10);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Should_Have_Error_When_Name_Is_Too_Long()
        {
            //Arrange
            var longName = new string('A', 151);
            var command = new UpdateProductCommand(Guid.NewGuid(), "Code", longName, new List<string>(), "Description", "img.jpg", 10);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Should_Have_Error_When_Price_Is_Zero()
        {
            //Arrange
            var command = new UpdateProductCommand(Guid.NewGuid(), "Code", "Valid Name", new List<string>(), "Description", "img.jpg", 0);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Test]
        public void Should_Not_Have_Errors_When_Command_Is_Valid()
        {
            //Arrange
            var command = new UpdateProductCommand(
                Guid.NewGuid(),
                "Code",
                "Valid Product Name",
                new List<string> { "Category1", "Category2" },
                "A nice product description",
                "image.jpg",
                25.50m);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}