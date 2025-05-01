using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Products.Features.CreateProduct
{
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", CreateProduct)
                .WithName("CreateProduct")
                .WithSummary("Create Product")
                .WithDescription("Create Product")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<Microsoft.AspNetCore.Http.IResult> CreateProduct(
            CreateProductRequest request,
            [FromServices] IMapper mapper,
            [FromServices] ISender sender)
        {
            var command = mapper.Map<CreateProductCommand>(request);
            var result = await sender.Send(command);
            return Results.Created($"/products/{result.Id}", result.Id);
        }
    }
}
