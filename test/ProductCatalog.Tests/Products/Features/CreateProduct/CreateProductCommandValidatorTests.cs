using ProductCatalog.Products.Features.CreateProduct;

namespace ProductCatalog.Tests.Products.Features.CreateProduct
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class CreateProductCommandValidatorTests
    {
        private CreateProductCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateProductCommandValidator();
        }

        [Test]
        public void Should_Have_Error_When_Code_Is_Null()
        {
            //Arrange
            var command = new CreateProductCommand(null!, "Name", new List<string> { "Cat1" }, "description", "img.jpg", 10);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Test]
        public void Should_Have_Error_When_Name_Is_Null()
        {
            //Arrange
            var command = new CreateProductCommand("Code", null!, new List<string> { "Cat1" }, "description", "img.jpg", 10);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            //Arrange
            var command = new CreateProductCommand("Code", "", new List<string> { "Cat1" }, "description", "img.jpg", 10);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Should_Have_Error_When_Category_Is_Empty()
        {
            //Arrange
            var command = new CreateProductCommand("Code", "Product", new List<string>(), "description", "img.jpg", 10);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Category);
        }

        [Test]
        public void Should_Have_Error_When_ImageFile_Is_Empty()
        {
            //Arrange
            var command = new CreateProductCommand("Code", "Product", new List<string> { "Cat1" }, "description", "", 10);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ImageFile);
        }

        [Test]
        public void Should_Have_Error_When_ImageFile_Is_Null()
        {
            //Arrange
            var command = new CreateProductCommand("Code", "Product", new List<string> { "Cat1" }, "description", null!, 10);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ImageFile);
        }

        [Test]
        public void Should_Have_Error_When_Price_Is_Zero()
        {
            //Arrange
            var command = new CreateProductCommand("Code", "Product", new List<string> { "Cat1" }, "description", "img.jpg", 0);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Test]
        public void Should_Have_Error_When_Price_Is_Negative()
        {
            //Arrange
            var command = new CreateProductCommand("Code", "Product", new List<string> { "Cat1" }, "description", "img.jpg", -5);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Test]
        public void Should_Not_Have_Errors_When_Command_Is_Valid()
        {
            //Arrange
            var command = new CreateProductCommand(
                "Code",
                "Valid Product",
                new List<string> { "Category1" },
                "A great product",
                "image.jpg",
                20.99m);

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
