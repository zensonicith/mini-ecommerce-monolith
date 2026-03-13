using Microsoft.AspNetCore.Http;
using MyApp.Application.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto> GetProductByIdAsync(int id);
        Task<ProductResponseDto> AddProductAsync(ProductRequestDto request, IFormFile? image, CancellationToken ct = default);
        Task<bool> UpdateProductAsync(int id, ProductRequestDto request, IFormFile? newImage, CancellationToken ct = default);
        Task<bool> DeleteProductAsync(int id);
    }
}
