namespace ProductCatalog.Products.Features.DeleteProduct
{
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", DeleteProduct)
                .WithName("DeleteProduct")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Product")
                .WithDescription("Deletes a product by its unique identifier.");
        }

        private static async Task<Microsoft.AspNetCore.Http.IResult> DeleteProduct(Guid id, ISender sender)
        {
            Result<bool> result = await sender.Send(new DeleteProductCommand(id));

            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { message = result.Error });
        }
    }
}
