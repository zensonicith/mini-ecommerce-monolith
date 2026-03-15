using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces;

public interface IFileStorageService
{
    Task<UploadImageResponseDto?> UploadImageAsync(UploadImageRequestDto requestDto, string prefix = "product",
        CancellationToken ct = default);

    Task DeleteImageAsync(string? imageUrl, CancellationToken ct = default);
}