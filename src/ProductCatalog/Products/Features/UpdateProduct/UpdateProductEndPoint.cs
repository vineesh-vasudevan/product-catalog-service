
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Products.Features.UpdateProduct
{
    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/{id}", UpdateProduct)
                .WithName("UpdateProduct")
                .WithSummary("Update Product")
                .WithDescription("Update Product")
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound);
        }

        private static async Task<Microsoft.AspNetCore.Http.IResult> UpdateProduct(
            Guid? id,
            [FromBody] UpdateProductRequest request,
            [FromServices] IMapper mapper,
            [FromServices] ISender sender)
        {
            if (id is not Guid validId || validId == Guid.Empty)
            {
                return Results.BadRequest(new { message = "Product ID is missing or invalid." });
            }

            var command = mapper.Map<UpdateProductCommand>(request, opt => opt.Items["Id"] = validId);

            Result<UpdateProductResponse> result = await sender.Send(command);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { message = result.Error });
        }
    }
}
