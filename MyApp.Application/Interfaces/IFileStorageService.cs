using Microsoft.AspNetCore.Http;

namespace MyApp.Application.Interfaces;

public interface IFileStorageService
{
    Task<string?> UploadImageAsync(IFormFile? file, string prefix = "product", CancellationToken ct = default);

    Task DeleteImageAsync(string? imageUrl, CancellationToken ct = default);
}