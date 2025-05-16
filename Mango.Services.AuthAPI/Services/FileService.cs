
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.IO;

namespace Mango.Services.AuthAPI.Services;

public class FileService(IWebHostEnvironment _webHostEnvironment) : IFileService
{

    private readonly string _imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"uploads\images");

    public async Task<string?> UploadImageAsync(IFormFile image, CancellationToken ct = default)
    {
        if (!Directory.Exists(_imagePath))
            Directory.CreateDirectory(_imagePath);

        var uniqueImageName = $"{Guid.CreateVersion7()}{Path.GetExtension(image.FileName)}";
        var filePath = Path.Combine(_imagePath, uniqueImageName);
        
        using var fileStream = new FileStream(filePath, FileMode.Create);
        await image.CopyToAsync(fileStream, ct);

        return uniqueImageName;
    }
    public Task<(FileStream? stream, string? contentType, string? fileName)> StreamImageAsync(string image, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(image))
        {
            return Task.FromResult<(FileStream?, string?, string?)>((null, null, null));
        }
        var fileName = Path.GetFileName(image);
        var imagePath = Path.Combine(_imagePath, fileName);

        if (!File.Exists(imagePath))
        {
            return Task.FromResult<(FileStream?, string?, string?)>((null, null, null));
        }
        var extension = Path.GetExtension(imagePath).ToLowerInvariant();
        var contentType = extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".webp" => "image/webp",
            _ => "application/octet-stream"
        };
        var stream = new FileStream(
             imagePath,
             FileMode.Open,
             FileAccess.Read,
             FileShare.Read,
             4096,
             FileOptions.Asynchronous
        );

        return Task.FromResult<(FileStream?, string?, string?)>((stream, contentType, fileName));
    }
    public async Task<string?> UpdateImageAsync(IFormFile newImage, string image, CancellationToken ct = default)
    {
        if (!string.IsNullOrEmpty(image))
        {
            await DeleteImageAsync(image);
        }

        var addResult = await UploadImageAsync(newImage, ct);

        return addResult;
    }
    public Task DeleteImageAsync(string image)
    {
        if (string.IsNullOrEmpty(image))
            return Task.CompletedTask;

        try
        {
            var fileName = Path.GetFileName(image);
            var imagePath = Path.Combine(_imagePath, fileName);

            if (!File.Exists(imagePath))
                return Task.CompletedTask;

            File.Delete(imagePath);
            return Task.CompletedTask;
        }
        catch
        {
            return Task.CompletedTask;
        }
    }
}
