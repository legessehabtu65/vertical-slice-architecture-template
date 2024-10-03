﻿namespace VSATemplate.Features.Products.UpdateProduct;

public sealed record UpdateProductRequest(
    Guid Id,
    string Name,
    string Description,
    List<string> Categories,
    decimal Price);
public sealed record UpdateProductResponse(bool IsSuccess);

public sealed class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = new UpdateProductCommand(
              request.Id,
              request.Name,
              request.Description,
              request.Categories,
              request.Price);

            var result = await sender.Send(command);

            var response = new UpdateProductResponse(result.IsSuccess);

            Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
