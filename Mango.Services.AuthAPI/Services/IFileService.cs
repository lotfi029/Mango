namespace Store.Services.AuthAPI.Services;

public interface IFileService
{
    Task<string?> UploadImageAsync(IFormFile image, CancellationToken ct = default);
    Task<(FileStream? stream, string? contentType, string? fileName)> StreamImageAsync(string image, CancellationToken ct = default);
    Task DeleteImageAsync(string imageName);
    Task<string?> UpdateImageAsync(IFormFile newImage, string image, CancellationToken ct = default);
}
