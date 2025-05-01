using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;

namespace ProductCatalog.Products.Features.GetProducts
{
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", GetProducts)
                .WithName("GetProducts")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Retrieve the list of available products.");
        }

        private static async Task<Microsoft.AspNetCore.Http.IResult> GetProducts(
            [AsParameters] GetProductsRequest request,
            [FromServices] IMapper mapper,
            [FromServices] ISender sender)
        {
            var query = mapper.Map<GetProductsQuery>(request);
            var result = await sender.Send(query);

            if (result == null || !result.Products.Any())
            {
                return Results.Ok(new GetProductsResponse(Array.Empty<Product>()));
            }

            return Results.Ok(result);
        }
    }
}
