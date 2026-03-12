using MyApp.Application.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto> GetProductByIdAsync(int id);
        Task<ProductResponseDto> AddProductAsync(ProductRequestDto request);
        Task<bool> UpdateProductAsync(int id, ProductRequestDto request);
        Task<bool> DeleteProductAsync(int id);
    }
}
