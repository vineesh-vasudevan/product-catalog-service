namespace ProductCatalog.Products.Features.GetProductById
{
    public class GetProductIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", GetProduct)
                .WithName("GetProductById")
                .WithSummary("Get Product By Id")
                .WithDescription("Get Product By Id")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<Microsoft.AspNetCore.Http.IResult> GetProduct(Guid id, ISender sender)
        {
            Result<GetProductByIdResponse> result = await sender.Send(new GetProductByIdQuery(id));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { message = result.Error });
        }
    }
}
