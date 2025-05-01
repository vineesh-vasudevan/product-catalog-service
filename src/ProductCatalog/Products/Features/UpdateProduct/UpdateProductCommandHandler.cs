using ProductCatalog.Products.Infrastructure.Repositories;

namespace ProductCatalog.Products.Features.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductRepository repository)
        : ICommandHandler<UpdateProductCommand, Result<UpdateProductResponse>>
    {
        public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await repository.GetById(command.Id, cancellationToken);

            if (product is null)
            {
                return Result.Failure<UpdateProductResponse>($"Product with ID {command.Id} was not found.");
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            await repository.Update(product, cancellationToken);
            return Result.Success(new UpdateProductResponse(product));
        }
    }
}
