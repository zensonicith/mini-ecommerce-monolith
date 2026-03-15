using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.Infrastructure.Services;

public class AzureBlobStorageService : IFileStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public AzureBlobStorageService(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("AzureBlobStorage:ConnectionString")
                               ?? throw new InvalidOperationException("AzureBlobStorage:ConnectionString not found");

        _containerName = configuration.GetValue<string>("AzureBlobStorage:ContainerName")
                         ?? "product-images";

        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    public async Task<UploadImageResponseDto?> UploadImageAsync(UploadImageRequestDto requestDto, string prefix = "product",
        CancellationToken ct = default)
    {
        if (requestDto.Image == null || requestDto.Image.Length == 0)
            return null;

        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: ct);

        var extension = Path.GetExtension(requestDto.Image.FileName).ToLowerInvariant();
        var blobName = $"{prefix}-{Guid.NewGuid():N}{extension}";

        var blobClient = containerClient.GetBlobClient(blobName);

        await using var stream = requestDto.Image.OpenReadStream();
        await blobClient.UploadAsync(stream, new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders { ContentType = requestDto.Image.ContentType }
        }, ct);

        var imageUrl = blobClient.Uri.ToString(); // URL đầy đủ

        return new UploadImageResponseDto()
        {
            ImageUrl = imageUrl
        };
    }

    public async Task DeleteImageAsync(string? imageUrl, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return;

        Uri uri;
        try
        {
            uri = new Uri(imageUrl);
        }
        catch
        {
            return;
        }

        // Lấy blob name từ URL
        var blobName = uri.Segments.LastOrDefault()?.TrimEnd('/');

        if (string.IsNullOrEmpty(blobName))
            return;

        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: ct);
    }
}