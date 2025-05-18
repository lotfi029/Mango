using Carter;
using Microsoft.AspNetCore.Mvc;
using Store.Services.AuthAPI.Services;

namespace Store.Services.AuthAPI.Endpoints;

public class FileEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/files")
            .WithTags("Files")
            .RequireAuthorization();

        group.MapGet("/image", GetImage)
            .WithName("GetImage");

    }

    private async Task<IResult> GetImage(
        string imageName,
        [FromServices] IFileService fileService,
        CancellationToken ct
        )
    {
        var result = await fileService.StreamImageAsync(imageName, ct);
        if (result.stream is null)
            return Results.NotFound();

        return TypedResults.File(result.stream, result.contentType, result.fileName);
    }
}
