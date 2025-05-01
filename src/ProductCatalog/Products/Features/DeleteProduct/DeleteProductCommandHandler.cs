using ProductCatalog.Products.Infrastructure.Repositories;

namespace ProductCatalog.Products.Features.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductRepository repository) : ICommandHandler<DeleteProductCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await repository.GetById(command.Id, cancellationToken);

            if (product is null)
            {
                return Result.Failure<bool>($"Product with ID {command.Id} was not found.");
            }

            await repository.Delete(command.Id, cancellationToken);
            return Result.Success(true);
        }
    }
}
